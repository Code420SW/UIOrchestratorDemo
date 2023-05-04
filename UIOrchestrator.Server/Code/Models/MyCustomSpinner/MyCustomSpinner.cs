using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Models.MyCustomSpinner
{
    public class MyCustomSpinner
    {
        /// <summary>
        /// Contains the base assembly path to where all custom spinner components are stored.
        /// Important that you update this to YOUR namespace.
        /// </summary>
        private const string assemblyNamespacePrefix = "Code420.CanXtracServer.Components.CustomComponents.CustomSpinner.MyCustomSpinners";

        /// <summary>
        /// Type value containing the <see cref="Type" /> for the spinner component.
        /// May be null if the assembly-qualified name of the custom spinner component
        /// can not be resolved.
        /// </summary>
        public Type SpinnerType { get; private set; }

        /// <summary>
        /// String value containing the validated CSS size style for the spinner height.
        /// Value is based on parameter passed to constructor adjusted as needed to fall within the custom spinner's limits.
        /// </summary>
        public string SpinnerHeight { get; private set; }

        /// <summary>
        /// String value containing the validated CSS size style for the spinner width.
        /// Value is based on parameter passed to constructor adjusted as needed to fall within the custom spinner's limits.
        /// </summary>
        public string SpinnerWidth { get; private set; }


        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="spinner">
        /// One of the <see cref="CustomSpinners"/> enum values indicating which
        /// of the predefined custom spinners is instantiated.
        /// </param>
        /// <param name="height">
        /// String value containing the desired height of the custom spinner.
        /// It is assumed the height is in the form of a CSS size style (e.g., 250px)
        /// </param>
        /// <param name="width">
        /// String value containing the desired height of the custom spinner.
        /// It is assumed the width is in the form of a CSS size style (e.g., 250px)
        /// </param>
        /// <remarks>
        /// The custom spinner component must exist on the assembly path defined by the
        /// <see cref="SpinnerType"/> constant. If the component can not be
        /// resolved in the assembly path, the <see cref="CustomSpinners"/> property will be null.
        /// </remarks>
        public MyCustomSpinner(CustomSpinners spinner, string height, string width)
        {
            SpinnerHeight = height;
            SpinnerWidth = width;

            GetSpinnerSettings(spinner);
        }


        /// <summary>
        /// Update the <see cref="SpinnerHeight"/> and <see cref="SpinnerWidth"/> properties
        /// based on the passed <see cref="CustomSpinners"/>
        /// </summary>
        /// <param name="spinner"></param>
        private void GetSpinnerSettings(CustomSpinners spinner)
        {
            switch(spinner) 
            {
                case CustomSpinners.SwingingBall:
                    SetSpinnerSize(200, 200, 100,100, 300, 300);
                    SpinnerType = Type.GetType($"{ assemblyNamespacePrefix }.SwingingBall");
                    break;
                
                case CustomSpinners.Sphere3dAnimatedFilling:
                    SetSpinnerSize(200, 200, 100,100, 300, 300);
                    SpinnerType = Type.GetType($"{ assemblyNamespacePrefix }.Sphere3dAnimatedFilling");
                    break;
                
                case CustomSpinners.ThreeBarLoader:
                    SetSpinnerSize(100, 100, 50,50, 150, 150);
                    SpinnerType = Type.GetType($"{ assemblyNamespacePrefix }.ThreeBarLoader");
                    break;

                default:
                    //  A CustomSpinners enum member is not handled. This is a developer error.
                    throw new ArgumentException($"The CustomSpinner enum element { spinner } is not handled.");
            }
        }


        //
        // Method to validate parameters. Add to it as needed.
        //
        /// <summary>
        /// Validates the custom spinner parameters set by the <see cref="GetSpinnerSettings"/>
        /// method. Sets the <see cref="SpinnerHeight"/> and <see cref="SpinnerWidth"/> properties.
        /// </summary>
        /// <param name="nominalHeight">
        /// Integer value containing the nominal height (in pixels) of the custom spinner.
        /// </param>
        /// <param name="nominalWidth">
        /// Integer value containing the nominal width (in pixels) of the custom spinner.
        /// </param>
        /// <param name="minHeight">
        /// Integer value containing the minimum height (in pixels) of the custom spinner.
        /// </param>
        /// <param name="minWidth">
        /// Integer value containing the minimum width (in pixels) of the custom spinner.
        /// </param>
        /// <param name="maxHeight">
        /// Integer value containing the maximum height (in pixels) of the custom spinner.
        /// </param>
        /// <param name="maxWidth">
        /// Integer value containing the maximum width (in pixels) of the custom spinner.
        /// </param>
        private void SetSpinnerSize(int nominalHeight, int nominalWidth, int minHeight, int minWidth, int maxHeight, int maxWidth)
        {
            // Parse the SpinnerHeight and SpinnerWidth properties as integers.
            // It is assumed each is in the form of a CSS size style (e.g., 250px)
            // If a good parse can't happen, fall back to nominal values
            if (int.TryParse(GetNumbers(SpinnerHeight), out int tempHeight) == false) tempHeight = nominalHeight;
            if (int.TryParse(GetNumbers(SpinnerWidth), out int tempWidth) == false) tempWidth = nominalWidth;

            // Sanity-check the sizes and adjust as needed
            if ((tempHeight < minHeight) || (tempHeight > maxHeight)) tempHeight = nominalHeight;
            if ((tempWidth < minWidth) || (tempWidth > maxWidth)) tempWidth = nominalWidth;

            // Reset the SpinnerHeight and SpinnerWidth properties
            SpinnerHeight = $"{ tempHeight }px";
            SpinnerWidth = $"{ tempWidth }px";
        }


        //
        // Helper method to parse integers out of a string
        //
        private  string GetNumbers(string input)
        {
            return new string(input.Where(char.IsDigit).ToArray());
        }
    }
}
