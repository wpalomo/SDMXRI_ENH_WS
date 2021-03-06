// -----------------------------------------------------------------------
// <copyright file="StructureRequestV21AdvancedController.cs" company="EUROSTAT">
//   Date Created : 2013-10-10
//   Copyright (c) 2009, 2015 by the European Commission, represented by Eurostat.   All rights reserved.
// 
// Licensed under the EUPL, Version 1.1 or � as soon they
// will be approved by the European Commission - subsequent
// versions of the EUPL (the "Licence");
// You may not use this work except in compliance with the
// Licence.
// You may obtain a copy of the Licence at:
// 
// https://joinup.ec.europa.eu/software/page/eupl 
// 
// Unless required by applicable law or agreed to in
// writing, software distributed under the Licence is
// distributed on an "AS IS" basis,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied.
// See the Licence for the specific language governing
// permissions and limitations under the Licence.
// </copyright>
// -----------------------------------------------------------------------
namespace Estat.Sri.Ws.Controllers.Controller
{
    using System.Linq;
    using System.ServiceModel.Channels;
    using System.Xml;

    using Estat.Nsi.AuthModule;
    using Estat.Sdmxsource.Extension.Manager;
    using Estat.Sri.Ws.Controllers.Constants;
    using Estat.Sri.Ws.Controllers.Extension;
    using Estat.Sri.Ws.Controllers.Properties;

    using Org.Sdmxsource.Sdmx.Api.Constants;
    using Org.Sdmxsource.Sdmx.Api.Exception;
    using Org.Sdmxsource.Sdmx.Api.Manager.Retrieval.Mutable;
    using Org.Sdmxsource.Sdmx.Api.Model.Mutable;
    using Org.Sdmxsource.Sdmx.Api.Model.Objects;
    using Org.Sdmxsource.Sdmx.Api.Util;
    using Org.Sdmxsource.Sdmx.Structureparser.Manager.Parsing;
    using Org.Sdmxsource.Sdmx.Structureparser.Workspace;
    using Org.Sdmxsource.Util.Io;

    /// <summary>
    /// The structure request SDMX v20 controller.
    /// </summary>
    /// <typeparam name="TWriter">
    /// The type of the output writer
    /// </typeparam>
    public class StructureRequestV21AdvancedController<TWriter> : QueryStructureController<TWriter>, 
                                                                  IController<XmlNode, TWriter>, 
                                                                  IController<IReadableDataLocation, TWriter>, 
                                                                  IController<Message, TWriter>
    {
        #region Fields

        /// <summary>
        ///     The AUTH structure search manager.
        /// </summary>
        private readonly IAuthAdvancedMutableStructureSearchManager _authStructureSearchManager;

        /// <summary>
        ///     The manager.
        /// </summary>
        private readonly IQueryParsingManager _manager;

        /// <summary>
        ///     The _root node
        /// </summary>
        private readonly XmlQualifiedName _rootNode;

        /// <summary>
        ///     The _structure search manager.
        /// </summary>
        private readonly IAdvancedMutableStructureSearchManager _structureSearchManager;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StructureRequestV21AdvancedController{TWriter}"/> class.
        /// </summary>
        /// <param name="responseGenerator">
        /// The response generator.
        /// </param>
        /// <param name="authStructureSearchManager">
        /// The authentication structure search manager.
        /// </param>
        /// <param name="structureSearchManager">
        /// The structure search manager.
        /// </param>
        /// <param name="principal">
        /// The principal.
        /// </param>
        /// <param name="soapOperation">
        /// The SOAP operation.
        /// </param>
        /// <exception cref="SdmxSemmanticException">
        /// Operation not accepted with query used
        /// </exception>
        public StructureRequestV21AdvancedController(
            IResponseGenerator<TWriter, ISdmxObjects> responseGenerator, 
            IAuthAdvancedMutableStructureSearchManager authStructureSearchManager, 
            IAdvancedMutableStructureSearchManager structureSearchManager, 
            DataflowPrincipal principal, 
            SoapOperation soapOperation)
            : base(responseGenerator, principal)
        {
            this._authStructureSearchManager = authStructureSearchManager;
            this._structureSearchManager = structureSearchManager;
            SdmxSchema sdmxSchemaV21 = SdmxSchema.GetFromEnum(SdmxSchemaEnumType.VersionTwoPointOne);
            this._manager = new QueryParsingManager(sdmxSchemaV21.EnumType);
            this._rootNode = soapOperation.GetQueryRootElementV21();
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Parse request from <paramref name="input"/>
        /// </summary>
        /// <param name="input">
        /// The reader for the SDMX-ML request
        /// </param>
        /// <returns>
        /// The <see cref="IStreamController{TWriter}"/>.
        /// </returns>
        public IStreamController<TWriter> ParseRequest(IReadableDataLocation input)
        {
            return this.ParseRequestPrivate(principal => this.GetMutableObjectsV21(input, principal));
        }

        /// <summary>
        /// Parse request from <paramref name="input"/>
        /// </summary>
        /// <param name="input">
        /// The reader for the SDMX-ML or REST request
        /// </param>
        /// <returns>
        /// The <see cref="IStreamController{TWriter}"/>.
        /// </returns>
        public IStreamController<TWriter> ParseRequest(XmlNode input)
        {
            using (IReadableDataLocation xmlReadable = new XmlDocReadableDataLocation(input))
            {
                return this.ParseRequest(xmlReadable);
            }
        }

        /// <summary>
        /// Parse request from <paramref name="input"/>
        /// </summary>
        /// <param name="input">
        /// The reader for the SDMX-ML or REST request
        /// </param>
        /// <returns>
        /// The <see cref="IStreamController{TWriter}"/>.
        /// </returns>
        public IStreamController<TWriter> ParseRequest(Message input)
        {
            if (input == null)
            {
                throw new SdmxSemmanticException(Resources.ErrorOperationNotAccepted);
            }

            using (IReadableDataLocation xmlReadable = input.GetReadableDataLocation(this._rootNode))
            {
                return this.ParseRequest(xmlReadable);
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets the mutable objects V20.
        /// </summary>
        /// <param name="input">
        /// The input.
        /// </param>
        /// <param name="dataflowPrincipal">
        /// The dataflow principal.
        /// </param>
        /// <returns>
        /// The <see cref="IMutableObjects"/>.
        /// </returns>
        /// <exception cref="SdmxSemmanticException">
        /// Operation not accepted
        /// </exception>
        private IMutableObjects GetMutableObjectsV21(IReadableDataLocation input, DataflowPrincipal dataflowPrincipal)
        {
            IQueryWorkspace queryWorkspace = this._manager.ParseQueries(input);

            if (queryWorkspace == null)
            {
                // throw new SdmxSemmanticException(Properties.Resources.MissingRegistryOrInvalidSoap);
                throw new SdmxSemmanticException(Resources.ErrorOperationNotAccepted);
            }

            IMutableObjects mutableObjects = dataflowPrincipal != null
                                                 ? this._authStructureSearchManager.GetMaintainables(queryWorkspace.ComplexStructureQuery, dataflowPrincipal.AllowedDataflows.ToList())
                                                 : this._structureSearchManager.GetMaintainables(queryWorkspace.ComplexStructureQuery);

            return mutableObjects;
        }

        #endregion
    }
}