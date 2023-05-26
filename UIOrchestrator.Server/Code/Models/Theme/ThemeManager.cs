using Code420.UIOrchestrator.Server.Code.Enums;

namespace Code420.UIOrchestrator.Server.Code.Models.Theme
{
    /// <summary>
    /// Responsible for managing the application theme colors. When instantiated, the
    /// applications color pallet is build as well as the definitions for the available
    /// themes defiled in the <see cref="ThemeType"/> enum.
    /// <remarks>
    /// Just so it is said, this class does violate the Open-Closed principal but, in
    /// reality, the application themes are set once and should not change.
    /// </remarks>
    /// </summary>
    public class ThemeManager : IThemeManager
    {
        /// <summary>
        /// A <see cref="Dictionary{TKey,TValue}"/> containing the CSS mappings for
        /// the light theme.
        /// TKey is a string object containing the application-specific CSS class name.
        /// TValue is a string object containing the mapping from the <see cref="colorPalette"/>
        /// to the application-specific CSS class name.
        /// </summary>
        private Dictionary<string, string> lightTheme { get; set; }
        
        /// <summary>
        /// A <see cref="Dictionary{TKey,TValue}"/> containing the CSS mappings for
        /// the dark theme.
        /// TKey is a string object containing the application-specific CSS class name.
        /// TValue is a string object containing the mapping from the <see cref="colorPalette"/>
        /// to the application-specific CSS class name.
        /// </summary>
        private Dictionary<string, string> darkTheme { get; set; }
        
        /// <summary>
        /// A <see cref="Dictionary{TKey,TValue}"/> containing the CSS mappings for
        /// the entire color pallet.
        /// TKey is a string object containing the CSS class name.
        /// TValue is a string object containing the corresponding CSS element.
        /// </summary>
        /// <remarks>
        /// The color pallet defines all CSS color elements that are mapped to
        /// generic CSS classes. The <see cref="lightTheme"/> and <see cref="darkTheme"/>
        /// properties use the color pallet definitions to convert generic CSS classes
        /// to application-specific CSS classes.
        /// </remarks>
        private Dictionary<string, string> colorPalette { get; set; }

        /// <summary>
        /// A <see cref="Dictionary{TKey,TValue}"/> containing the CSS mappings for
        /// the active theme.
        /// </summary>
        private Dictionary<string, string> ActiveTheme { get; set; }

        
        /// <summary>
        /// Class constructor.
        /// </summary>
        /// <param name="initialThemeType">
        /// One of the <see cref="ThemeType"/> enum elements.
        /// If omitted the default value is <see cref="ThemeType.Light"/>.
        /// </param>
        /// <remarks>
        /// Ensures the <see cref="colorPalette"/>, <see cref="lightTheme"/> and'
        /// <see cref="darkTheme"/>properties are initialized.
        /// Sets the <see cref="ActiveTheme"/> to the light theme.
        /// </remarks>
        public ThemeManager(ThemeType initialThemeType = ThemeType.Light)
        {
            LoadColorPalette();
            InitializeThemes();
            ActiveTheme = (initialThemeType is ThemeType.Light) ? lightTheme : darkTheme;
        }

        /// <summary>
        /// Responsible for setting the active theme.
        /// </summary>
        /// <param name="type">
        /// One of the <see cref="ThemeType"/> enum elements.
        /// </param>
        /// <remarks>
        /// The method must be updated to handle additional themes.
        /// Unhandled themes will default to the <see cref="ThemeType.Light"/> theme.
        /// </remarks>
        public void SetThemeType(ThemeType type)
        {
            ActiveTheme = type switch
            {
                ThemeType.Light => lightTheme,
                ThemeType.Dark => darkTheme,
                _ => lightTheme
            };
        }

        /// <summary>
        /// Retrieves the CSS element associated with the passed CSS class for the
        /// active theme.
        /// </summary>
        /// <param name="key">
        /// The TKey value for the <see cref="ActiveTheme"/> dictionary object.
        /// </param>
        /// <returns>
        /// The TValue associated with the TKey value for the <see cref="ActiveTheme"/>
        /// dictionary object.
        /// </returns>
        /// <remarks>
        /// In the event the TKey value for the <see cref="ActiveTheme"/> dictionary object
        /// can not be resolved, the return value is "red".
        /// </remarks>
        public string GetThemeItem(string key)
        {
            ActiveTheme.TryGetValue(key, out var item);
            return item ?? "red";
        }


        /// <summary>
        /// Build the <see cref="Dictionary{TKey,TValue}"/> object representing
        /// the color pallet for the application.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The color pallet defines the color set for the application. Changes to the
        /// over all colors used in the application are defined here.
        /// </para>
        /// <para>
        /// The color pallet defines foreground, background, accent, black, white
        /// and other generic CSS colors.
        /// </para>
        /// <para>
        /// Each color contains various shading variations ranging from 100 (lightest)
        /// to 900 (darkest).
        /// </para>
        /// <para>
        /// The color pallet is not directly referenced by the application. Its elements
        /// are used to construct the specific themes.
        /// </para>
        /// </remarks>
        private void LoadColorPalette()
        {
            colorPalette = new Dictionary<string, string>()
            {
                {"fg-900", "rgb(0, 141, 119)" },        // darker green
                {"fg-700", "rgb(50, 151, 131)" },
                {"fg-500", "rgb(119, 182, 166)" },
                {"fg-300", "rgb(178, 213, 203)" },
                {"fg-100", "rgb(236, 245, 242)" },      //  lighter green

                {"bg-900", "rgb(141, 101, 0)" },        //  darker brown
                {"bg-900-40", "rgba(141, 101, 0, 40)" },
                {"bg-700", "rgb(189, 154, 99)" },
                {"bg-500", "rgb(210, 182, 142)" },
                {"bg-300", "rgb(229, 211, 186)" },
                {"bg-100", "rgb(247, 240, 232)" },      //  light brown

                {"accent1-900", "rgb(141, 31, 0)" },    //  darker red
                {"accent1-700", "rgb(194, 114, 88)" },
                {"accent1-500", "rgb(215, 153, 133)" },
                {"accent1-300", "rgb(234, 193, 180)" },
                {"accent1-100", "rgb(249, 234, 230)" }, //  lighter red

                {"accent2-900", "rgb(59, 0, 141)" },    //  darker purple
                {"accent2-700", "rgb(138, 98, 184)" },
                {"accent2-500", "rgb(173, 141, 205)" },
                {"accent2-300", "rgb(206, 185, 225)" },
                {"accent2-100", "rgb(239, 231, 245)" }, //  lighter purple

                {"black-900", "rgb(20, 20, 20)" },      // darker black
                {"black-700", "rgb(37, 37, 37)" },
                {"black-500", "rgb(95, 95, 95)" },
                {"black-300", "rgb(160, 160, 160)" },
                {"black-100", "rgb(231, 231, 231)" },   //  lighter black

                {"white-900", "rgb(246,246,246)" },     // darker white
                {"white-700", "rgb(249,249,249)" },
                {"white-500", "rgb(250,250,250)" },
                {"white-500-40", "rgba(250,250,250,40)" },
                {"white-300", "rgb(252,252,252)" },
                {"white-100", "rgb(254,254,254)" },     //  lighter white

                {"css-inherit", "inherit" },
                {"css-transparent", "transparent" }
            };
        }
        
        
        /// <summary>
        /// Builds the theme-specific <see cref="Dictionary{TKey,TValue}"/> objects.
        /// </summary>
        /// <remarks>
        /// <para>
        /// The dictionary TKey values is the application-specific CSS class.
        /// The TValue value is extracted from the <see cref="colorPalette"/> object.
        /// </para>
        /// <para>
        /// Changes to specific theming elements are made here.
        /// </para>
        /// </remarks>
        private void InitializeThemes()
        {
            lightTheme = new Dictionary<string, string>
            {
                ["header1--bg"] = colorPalette["bg-700"],
                ["header2--bg"] = colorPalette["bg-700"],
                ["footer--bg"] = colorPalette["bg-700"],
                ["border__main--color"] = colorPalette["accent1-700"],
                
                ["tab__container--bg"] = colorPalette["bg-100"],
                ["tab__container--fg"] = colorPalette["fg-900"],
                ["tab__header--bg"] = colorPalette["bg-300"],
                ["tab__header-active-border--color"] = colorPalette["fg-900"],
                ["tab__header-inactive-border--color"] = colorPalette["fg-700"],
                ["tab__secondary-header--bg"] = colorPalette["fg-300"],
                ["tab__secondary-header-active-border--color"] = colorPalette["bg-900"],
                ["tab__secondary-header-inactive-border--color"] = colorPalette["bg-700"],
                ["tab__text-active--font-color"] = colorPalette["fg-900"],
                ["tab__text-inactive--font-color"] = colorPalette["fg-900"],
                ["tab__text-active-hover--font-color"] = colorPalette["accent1-900"],
                ["tab__text-inactive-hover--font-color"] = colorPalette["accent1-900"],
                
                ["sidebar--bg"] = colorPalette["bg-300"],
                ["sidebar__border--color"] = colorPalette["accent1-700"],
                ["sidebar__backdrop--bg"] = colorPalette["accent1-300"],
                
                ["vmenu__item-normal--font-color"] = colorPalette["fg-900"],
                ["vmenu__item-normal--background-color"] = colorPalette["css-transparent"],
                ["vmenu__item-active--font-color"] = colorPalette["accent1-900"],
                ["vmenu__item-active--background-color"] = colorPalette["css-inherit"],
                ["vmenu__icon--font-color"] = colorPalette["css-inherit"],
                ["vmenu__caret--font-color"] = colorPalette["css-inherit"],
                ["vmenu__separator--border-color"] = colorPalette["accent1-500"],
                ["vmenu__popup--font-color"] = colorPalette["fg-900"],
                ["vmenu__popup--background-color"] = colorPalette["bg-300"],
                ["vmenu__popup--border-color"] = colorPalette["accent1-700"],
                
                ["hmenu__item-normal--font-color"] = colorPalette["white-700"],
                ["hmenu__item-normal--background-color"] = colorPalette["css-transparent"],
                ["hmenu__item-active--font-color"] = colorPalette["accent1-900"],
                ["hmenu__item-active--background-color"] = colorPalette["css-inherit"],
                ["hmenu__icon--font-color"] = colorPalette["css-inherit"],
                ["hmenu__caret--font-color"] = colorPalette["css-inherit"],
                ["hmenu__separator--border-color"] = colorPalette["accent1-500"],
                ["hmenu__popup--font-color"] = colorPalette["fg-900"],
                ["hmenu__popup--background-color"] = colorPalette["bg-300"],
                ["hmenu__popup--border-color"] = colorPalette["accent1-700"],
                
                ["button-std__normal--background-color"] = colorPalette["fg-700"],
                ["button-std__normal--border-color"] = colorPalette["bg-900"],
                ["button-std__active--background-color"] = colorPalette["fg-700"],
                ["button-std__active--border-color"] = colorPalette["accent1-900"],
                ["button-std__normal--font-color"] = colorPalette["white-500"],
                ["button-std-icon__normal--font-color"] = colorPalette["white-500"],
                
                ["element__normal--background-color"] = colorPalette["white-700"],
                ["element__normal--border-color"] = colorPalette["bg-900"],
                ["element__active--border-color"] = colorPalette["bg-500"],
                ["element__normal--font-color"] = colorPalette["fg-900"],
                ["element__disabled--font-color"] = colorPalette["fg-500"],
                
                ["button-help__normal--border-color"] = colorPalette["bg-900"],
                ["button-help__normal--background-color"] = colorPalette["white-900"],
                ["button-help__icon--font-color"] = colorPalette["fg-900"],
                ["button-help__icon--drop-shadow-color"] = colorPalette["bg-900-40"],
                
                ["tooltip-help__background-color"] = colorPalette["fg-900"],
                ["tooltip-help__header-font-color"] = colorPalette["white-900"],
                ["tooltip-help__bottom-border-color"] = colorPalette["bg-700"],
                ["tooltip-help__title-font-color"] = colorPalette["fg-900"],
                
                ["dialog-help__background-color"] = colorPalette["bg-300"],
                ["dialog-help__title-font-color"] = colorPalette["bg-900"],
                ["dialog-help__subtitle-font-color"] = colorPalette["bg-900"],
                ["dialog-help__header-background-color"] = colorPalette["fg-300"],
                ["dialog-help__icon-font-color"] = colorPalette["white-500"]
            };


            darkTheme = new Dictionary<string, string>
            {
                ["header1--bg"] = colorPalette["black-500"],
                ["header2--bg"] = colorPalette["black-500"],
                ["footer--bg"] = colorPalette["black-500"],
                ["border__main--color"] = colorPalette["accent2-700"],
                
                ["tab__container--bg"] = colorPalette["black-900"], //black-900
                ["tab__container--fg"] = colorPalette["white-500"], //white-500
                ["tab__header--bg"] = colorPalette["black-300"],
                ["tab__header-active-border--color"] = colorPalette["accent2-900"],
                ["tab__header-inactive-border--color"] = colorPalette["accent2-700"],
                ["tab__secondary-header--bg"] = colorPalette["fg-300"],
                ["tab__secondary-header-active-border--color"] = colorPalette["bg-900"],
                ["tab__secondary-header-inactive-border--color"] = colorPalette["bg-700"],
                ["tab__text-active--font-color"] = colorPalette["accent2-900"],
                ["tab__text-inactive--font-color"] = colorPalette["accent2-900"],
                ["tab__text-active-hover--font-color"] = colorPalette["white-700"],
                ["tab__text-inactive-hover--font-color"] = colorPalette["white-700"],
                
                ["sidebar--bg"] = colorPalette["black-300"],
                ["sidebar__border--color"] = colorPalette["accent2-700"],
                ["sidebar__backdrop--bg"] = colorPalette["accent2-300"],
                
                ["vmenu__item-normal--font-color"] = colorPalette["accent2-900"],
                ["vmenu__item-normal--background-color"] = colorPalette["css-transparent"],
                ["vmenu__item-active--font-color"] = colorPalette["white-700"],
                ["vmenu__item-active--background-color"] = colorPalette["css-inherit"],
                ["vmenu__icon--font-color"] = colorPalette["css-inherit"],
                ["vmenu__caret--font-color"] = colorPalette["css-inherit"],
                ["vmenu__separator--border-color"] = colorPalette["accent2-500"],
                ["vmenu__popup--font-color"] = colorPalette["accent2-900"],
                ["vmenu__popup--background-color"] = colorPalette["black-300"],
                ["vmenu__popup--border-color"] = colorPalette["accent2-700"],
                
                ["hmenu__item-normal--font-color"] = colorPalette["white-700"],
                ["hmenu__item-normal--background-color"] = colorPalette["css-transparent"],
                ["hmenu__item-active--font-color"] = colorPalette["accent2-900"],
                ["hmenu__item-active--background-color"] = colorPalette["css-inherit"],
                ["hmenu__icon--font-color"] = colorPalette["css-inherit"],
                ["hmenu__caret--font-color"] = colorPalette["css-inherit"],
                ["hmenu__separator--border-color"] = colorPalette["accent2-500"],
                ["hmenu__popup--font-color"] = colorPalette["white-500"],
                ["hmenu__popup--background-color"] = colorPalette["black-300"],
                ["hmenu__popup--border-color"] = colorPalette["accent2-700"],
                
                ["button-std__normal--background-color"] = colorPalette["black-300"],
                ["button-std__normal--border-color"] = colorPalette["accent2-700"],
                ["button-std__active--background-color"] = colorPalette["black-700"],
                ["button-std__active--border-color"] = colorPalette["accent2-500"],
                ["button-std-icon__normal--font-color"] = colorPalette["white-500"],
                
                ["element__normal--background-color"] = colorPalette["white-700"],
                ["element__normal--border-color"] = colorPalette["accent2-700"],
                ["element__active--border-color"] = colorPalette["black-500"],
                ["element__normal--font-color"] = colorPalette["white-500"],
                ["element__disabled--font-color"] = colorPalette["white-100"],
                
                ["button-help__normal--border-color"] = colorPalette["accent2-700"],
                ["button-help__normal--background-color"] = colorPalette["black-900"],
                ["button-help__icon--font-color"] = colorPalette["white-500"],
                ["button-help__icon--drop-shadow-color"] = colorPalette["bg-900-40"],
                
                ["tooltip-help__background-color"] = colorPalette["white-500"],
                ["tooltip-help__header-font-color"] = colorPalette["black-900"],
                ["tooltip-help__bottom-border-color"] = colorPalette["black-500"],
                ["tooltip-help__title-font-color"] = colorPalette["white-500"],
                
                ["dialog-help__background-color"] = colorPalette["black-300"],
                ["dialog-help__title-font-color"] = colorPalette["white-500"],
                ["dialog-help__subtitle-font-color"] = colorPalette["white-500"],
                ["dialog-help__header-background-color"] = colorPalette["white-500"],
                ["dialog-help__icon-font-color"] = colorPalette["white-500"]
            };
        }
    }
}
