﻿// -----------------------------------------------------------------------
// <copyright file="CategorySchemeImportEngine.cs" company="EUROSTAT">
//   Date Created : 2013-04-09
//   Copyright (c) 2009, 2015 by the European Commission, represented by Eurostat.   All rights reserved.
// 
// Licensed under the EUPL, Version 1.1 or – as soon they
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
namespace Estat.Sri.MappingStore.Store.Engine
{
    using System.Collections.Generic;
    using System.Globalization;

    using Estat.Ma.Model.StoredProcedure;
    using Estat.Sri.MappingStore.Store.Factory;
    using Estat.Sri.MappingStore.Store.Model;
    using Estat.Sri.MappingStoreRetrieval.Manager;

    using log4net;

    using Org.Sdmxsource.Sdmx.Api.Model.Objects.CategoryScheme;

    /// <summary>
    /// The category scheme import engine.
    /// </summary>
    public class CategorySchemeImportEngine : ItemSchemeImportEngine<ICategorySchemeObject, ICategoryObject>
    {
        #region Static Fields

        /// <summary>
        ///     The log.
        /// </summary>
        private static readonly ILog _log = LogManager.GetLogger(typeof(CategorySchemeImportEngine));

        /// <summary>
        /// The _stored procedures.
        /// </summary>
        private static readonly StoredProcedures _storedProcedures;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes static members of the <see cref="CategorySchemeImportEngine"/> class.
        /// </summary>
        static CategorySchemeImportEngine()
        {
            _storedProcedures = new StoredProcedures();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySchemeImportEngine"/> class. 
        /// </summary>
        /// <param name="database">
        /// The mapping store database instance.
        /// </param>
        public CategorySchemeImportEngine(Database database)
            : base(database)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategorySchemeImportEngine"/> class. 
        /// </summary>
        /// <param name="database">
        /// The mapping store database instance.
        /// </param>
        /// <param name="factory">
        /// The <see cref="IItemImportEngine{T}"/> factory. Optional
        /// </param>
        public CategorySchemeImportEngine(Database database, IItemImportFactory<ICategoryObject> factory)
            : base(database, factory)
        {
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Insert the specified <paramref name="maintainable"/> to the mapping store with <paramref name="state"/>
        /// </summary>
        /// <param name="state">
        /// The MAPPING STORE connection and transaction state
        /// </param>
        /// <param name="maintainable">
        /// The maintainable.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable{T}"/>.
        /// </returns>
        public override ArtefactImportStatus Insert(DbTransactionState state, ICategorySchemeObject maintainable)
        {
            _log.DebugFormat(CultureInfo.InvariantCulture, "Importing artefact {0}", maintainable.Urn);
            var artefactStoredProcedure = _storedProcedures.InsertCategoryScheme;
            return this.InsertInternal(state, maintainable, artefactStoredProcedure);
        }

        #endregion
    }
}