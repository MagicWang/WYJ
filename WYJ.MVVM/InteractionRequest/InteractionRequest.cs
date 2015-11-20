//===================================================================================
// Microsoft patterns & practices
// Composite Application Guidance for Windows Presentation Foundation and Silverlight
//===================================================================================
// Copyright (c) Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// The example companies, organizations, products, domain names,
// e-mail addresses, logos, people, places, and events depicted
// herein are fictitious.  No association with any real company,
// organization, product, domain name, email address, logo, person,
// places, or events is intended or should be inferred.
//===================================================================================
using System;

namespace Psap.Client.CustomControlLibrary.InteractionRequest
{
    /// <summary>
    /// Implementation of the <see cref="IInteractionRequest"/> interface.
    /// </summary>
    public class InteractionRequest
    {
        /// <summary>
        /// Fired when interaction is needed.
        /// </summary>
        public event EventHandler<InteractionRequestedEventArgs> Raised;
        public void Raise()
        {
            this.Raise(null);
        }
        /// <summary>
        /// Fires the Raised event.
        /// </summary>
        /// <param name="context">The context for the interaction request.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void Raise(object param)
        {
            this.Raise(param, null);
        }

        /// <summary>
        /// Fires the Raised event.
        /// </summary>
        /// <param name="context">The context for the interaction request.</param>
        /// <param name="callback">The callback to execute when the interaction is completed.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate")]
        public void Raise(object param, Action callback)
        {
            if (Raised != null)
                Raised(this, new InteractionRequestedEventArgs(param, callback));
        }
    }
}
