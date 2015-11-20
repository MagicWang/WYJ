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
using System.Windows;
using System.Windows.Interactivity;

namespace Psap.Client.CustomControlLibrary.InteractionRequest
{
    /// <summary>
    /// Custom event trigger for using with <see cref="IInteractionRequest"/> objects.
    /// </summary>
    /// <remarks>
    /// The standard <see cref="EventTrigger"/> class can be used instead, as long as the 'Raised' event 
    /// name is specified.
    /// </remarks>
    public class InteractionRequestTrigger : System.Windows.Interactivity.EventTrigger
    {
        public event EventHandler<InteractionRequestedEventArgs> Raised;
        private object sourceObject = null;

        /// <summary>
        /// Specifies the name of the Event this EventTriggerBase is listening for.
        /// </summary>
        /// <returns>This implementation always returns the Raised event name for ease of connection with <see cref="IInteractionRequest"/>.</returns>
        protected override string GetEventName()
        {
            return "Raised";
        }

        /// <summary>
        /// Called after the trigger is attached to an AssociatedObject.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();

            FrameworkElement element = this.AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded += AssociatedObject_Loaded;
                element.Unloaded += AssociatedObject_Unloaded;
            }
        }

        /// <summary>
        /// Called when the trigger is being dettached from its AssociatedObject, but before it has actually occurred.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();

            FrameworkElement element = this.AssociatedObject as FrameworkElement;
            if (element != null)
            {
                element.Loaded -= AssociatedObject_Loaded;
                element.Unloaded -= AssociatedObject_Unloaded;
            }
        }

        private void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            this.sourceObject = this.SourceObject;
            this.SourceObject = null;
        }

        private void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.SourceObject == null && this.sourceObject != null)
            {
                this.SourceObject = this.sourceObject;
            }
        }
        protected override void OnEvent(EventArgs eventArgs)
        {
            base.OnEvent(eventArgs);
            if (Raised != null)
                Raised(this, eventArgs as InteractionRequestedEventArgs);
        }
    }
}
