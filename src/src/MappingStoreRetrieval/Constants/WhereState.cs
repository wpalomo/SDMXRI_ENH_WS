﻿// -----------------------------------------------------------------------
// <copyright file="WhereState.cs" company="EUROSTAT">
//   Date Created : 2013-03-20
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
namespace Estat.Sri.MappingStoreRetrieval.Constants
{
    /// <summary>
    /// The SQL WHERE state.
    /// </summary>
    internal enum WhereState
    {
        /// <summary>
        /// No WHERE clause exists.
        /// </summary>
        Nothing, 

        /// <summary>
        /// The WHERE clause exists but nothing else.
        /// </summary>
        Where, 

        /// <summary>
        /// The WHERE clause exists together with clauses.
        /// </summary>
        And
    }
}