﻿/*
* Copyright (c) 2015 Benton Stark
* This file is part of the Starksoft Aspen library.
*
* Starksoft Aspen is free software: you can redistribute it and/or modify
* it under the terms of the GNU General Public License as published by
* the Free Software Foundation, either version 3 of the License, or
* (at your option) any later version.
* 
* Starksoft Aspen is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
* GNU General Public License for more details.
* 
* You should have received a copy of the GNU General Public License
* along with Starksoft Aspen.  If not, see <http://www.gnu.org/licenses/>.
*   
*/

using System;
using System.Runtime.Serialization;

namespace Starksoft.Aspen.Ftps
{

    /// <summary>
    /// This exception is thrown when a server side file hashing algorithm requested is invalid.
    /// </summary>
    [Serializable()]
    public class FtpsHashingInvalidAlgorithmException : FtpsHashingException
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public FtpsHashingInvalidAlgorithmException()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message text.</param>
        public FtpsHashingInvalidAlgorithmException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message text.</param>
        /// <param name="innerException">The inner exception object.</param>
        public FtpsHashingInvalidAlgorithmException(string message, Exception innerException)
            :
           base(message, innerException)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="info">Serialization information.</param>
        /// <param name="context">Stream context information.</param>
        protected FtpsHashingInvalidAlgorithmException(SerializationInfo info,
           StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message text.</param>
        /// <param name="response">Ftp response object.</param>
        /// <param name="innerException">The inner exception object.</param>
        public FtpsHashingInvalidAlgorithmException(string message, FtpsResponse response, Exception innerException)
            : base(message, response, innerException)
        { }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">Exception message text.</param>
        /// <param name="response">Ftp response object.</param>
        public FtpsHashingInvalidAlgorithmException(string message, FtpsResponse response)
            : base(message, response)
        { }

    }

}
