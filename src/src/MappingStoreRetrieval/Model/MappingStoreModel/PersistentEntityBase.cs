﻿// -----------------------------------------------------------------------
// <copyright file="PersistentEntityBase.cs" company="EUROSTAT">
//   Date Created : 2013-04-10
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
namespace Estat.Sri.MappingStoreRetrieval.Model.MappingStoreModel
{
    /// <summary>
    /// Abstract class for all entities that are contained in the database
    /// </summary>
    public abstract class PersistentEntityBase
    {
        #region Constants and Fields

        /// <summary>
        /// The unique entity identifier
        /// </summary>
        private readonly long _sysId;

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="PersistentEntityBase"/> class.
        /// </summary>
        /// <param name="sysId">
        /// The unique entity identifier
        /// </param>
        protected PersistentEntityBase(long sysId)
        {
            this._sysId = sysId;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the unique entity identifier
        /// </summary>
        public long SysId
        {
            get
            {
                return this._sysId;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Overrides the Equals method from the Object class.
        /// </summary>
        /// <param name="obj">
        /// right hand operand
        /// </param>
        /// <returns>
        /// Returns true if the objects are identique
        /// </returns>
        public override bool Equals(object obj)
        {
            return this.Equals(obj as PersistentEntityBase);
        }

        /// <summary>
        /// The method is used to compare the <see cref="PersistentEntityBase"/>
        /// object with the currrent object
        /// Two PersistenEntities are equal if their unique identifiers are equal
        /// </summary>
        /// <param name="other">
        /// The <see cref="PersistentEntityBase"/> 
        /// to compare with
        /// </param>
        /// <returns>
        /// Returns true if the objects are identique
        /// </returns>
        public bool Equals(PersistentEntityBase other)
        {
            if (other == null)
            {
                return false;
            }

            return this.GetType() == other.GetType() && this._sysId == other._sysId;
        }

        /// <summary>
        /// Overrides the GetHashCode method from the Object class.
        /// </summary>
        /// <returns>
        /// The <see cref="PersistentEntityBase._sysId"/>
        /// </returns>
        public override int GetHashCode()
        {
            return (int)this._sysId;
        }

        #endregion

        /*
        #region Operators

        /// <summary>
        /// Overrides the equals operator from the Object class. 
        /// Two PersistenEntities are equal if their id are equal
        /// </summary>
        /// <param name="firstEntity">left hand operand</param>
        /// <param name="secondEntity">right hand operand</param>
        /// <returns>Returns true if the objects are equals</returns>
        public static bool operator ==(PersistentEntityBase firstEntity, PersistentEntityBase secondEntity)
        {
            return ReferenceEquals(firstEntity, null) ? ReferenceEquals(secondEntity, null) : firstEntity.Equals(secondEntity);
        }

        /// <summary>
        /// Overrides the not equals operator from the Object class. 
        /// Two PersistenEntities are equal if their id are not equal
        /// </summary>
        /// <param name="firstEntity">left hand operand</param>
        /// <param name="secondEntity">right hand operand</param>
        /// <returns>Returns true if the objects are not equals</returns>
        public static bool operator !=(PersistentEntityBase firstEntity, PersistentEntityBase secondEntity)
        {
            return !(firstEntity == secondEntity);
        }
        
        #endregion
 */
    }
}