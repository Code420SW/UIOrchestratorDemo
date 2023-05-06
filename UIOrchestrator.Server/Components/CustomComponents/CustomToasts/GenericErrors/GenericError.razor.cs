using Code420.CanXtracServer.Components.BaseComponents.ToastBase;
using Microsoft.AspNetCore.Components;
using Syncfusion.Blazor.Notifications;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Code420.CanXtracServer.Components.CustomComponents.CustomToasts.GenericErrors
{
    /// <summary>
    /// A Toast used to display "generic" error messages.
    /// <para>
    /// Uses the <see cref="ToastBase"/> component to display an error message.
    /// The Toast is configured with a five second time out and will close when
    /// the user clicks on the Toast.
    /// </para>
    /// <para>
    /// The component exposes <see cref="Content"/>, <see cref="Target"/> and <see cref="Title"/>
    /// parameters used to customize the Toast.
    /// </para>
    /// <para>
    /// The component handles the <see cref="ToastBase.OnOpen"/> event callback to set the
    /// <see cref="Content"/>, <see cref="Target"/> and <see cref="Title"/> elements.
    /// </para>
    /// <para>
    /// The component does not expose any CSS parameters.
    /// </para>
    /// <para>
    /// The public method <see cref="ShowAsync(ToastModel)"/> is provided to allow the consumer
    /// to show the Toast. Note that the consumer should set the <see cref="Content"/>, 
    /// <see cref="Target"/> and <see cref="Title"/> parameters prior to invoking this method.
    /// </para>
    /// <para>
    /// The public method <see cref="HideAsync"/> is provided to allow the consumer to hide the Toast.
    /// </para>
    /// <remarks>
    /// The following parameters must be set:<br />
    /// <see cref="Content"/> -- The toast message.<br />
    /// <see cref="Title"/> -- The title element of the toast.<br />
    /// <see cref="Target"/> -- The parent container within which the toast is displayed.
    /// </remarks>
    /// </summary>
    
    public partial class GenericError : ComponentBase
    {

        #region Component Parameters

        #region Base Parameters

        /// <summary>
        /// String value that specifies the content to be displayed on the Toast. 
        /// Accepts selectors, string values and HTML elements.
        /// Default value is null.
        /// </summary>
        [Parameter]
        public string Content { get; set; }

        /// <summary>
        /// String value specifying the CSS ID of the target container where the Toast to be displayed. 
        /// Based on the target, the positions such as Left, Top will be applied to the Toast.
        /// The ID must be prefixed with the '#' symbol (e.g., #mybody).
        /// The default value is null, which refers the document.body element.
        /// </summary>
        [Parameter]
        public string Target { get; set; }

        /// <summary>
        /// String value specifying the title to be displayed on the Toast. 
        /// Works only with string values.
        /// Default value is null.
        /// </summary>
        /// <remarks>
        /// Non-null value causes injection of e-toast-title div
        /// </remarks>
        [Parameter]
        public string Title { get; set; }

        #endregion

        #endregion

        
        #region Callback Events Invoked from Underlying Components

        /// <summary>
        /// Handles the OnOpen event from <see cref="ToastBase"/>.
        /// Responsible for updating dynamic content from the parent which is passed
        /// through this component's parameter.
        /// </summary>
        /// <param name="args">
        /// A <see cref="ToastBeforeOpenArgs"/> object whose Options property contains the current settings
        /// for some of the ToastBase (SfToast) parameters.
        /// </param>
        /// <remarks>
        /// This methodology is more elegant and reliable than trying to force-change component parameters 
        /// and have them flow down to children. Change the dynamic elements and leave the static ones alone.
        /// </remarks>
        private void OnOpenHandler(ToastBeforeOpenArgs args)
        {
            args.Options.Content = Content;
            args.Options.Title = Title;
            args.Options.Target = Target;
        }
        
        #endregion


        #region Instance Variables

        private ToastBase toast;

        #endregion


        #region Public Methods Providing Access to the Underlying Components to the Consumer

        /// <summary>
        /// Show the Toast element.
        /// </summary>
        /// <param name="toastModel">
        /// Optionally accepts a <see cref="ToastModel"/> parameter in which all Toast parameters are defined.
        /// </param>
        public async Task ShowAsync(ToastModel toastModel = null)
        {
            await toast.ShowAsync(toastModel);
        }

        /// <summary>
        /// Hides the Toast defined by this component.
        /// </summary>
        public async Task HideAsync()
        {
            await toast.HideAsync();
        }

        /// <summary>
        /// Responsible for hiding all open Toast elements.
        /// </summary>
        public async Task HideAllAsync()
        {
            await toast.HideAllAsync();
        }

        #endregion
    }
}
