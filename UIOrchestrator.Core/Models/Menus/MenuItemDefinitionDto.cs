using Code420.UIOrchestrator.Core.Models.Enums;

namespace Code420.UIOrchestrator.Core.Models.Menus
{
    public class MenuItemDefinitionDto
    {
        /// <summary>
        /// String value containing a unique identifier used by consumers of MenuItemDefinitionDto.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemCode { get; init; } = string.Empty;

        /// <summary>
        /// String value containing the displayed menu item text.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemText { get; init; } = string.Empty;

        /// <summary>
        /// String value containing a unique values used to establish the menu hierarchy.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemId { get; init; } = string.Empty;

        /// <summary>
        /// String value containing the relative path to the component loaded when the
        /// menu item is selected. The path is relative to the base path defined by the
        /// <see cref="UIOrchestratorConstants.UIOrchestratorConstants.OrchestratorTabBaseNamespace"/> constant.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemComponentName { get; init; }

        /// <summary>
        /// One of the values in the <see cref="MenuItemScope"/> enum
        /// specifying which menu(s) the menu item applies to.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public MenuItemScope MenuItemScope { get; init; } = MenuItemScope.AllMenus;

        /// <summary>
        /// This contains the list of permissions required to access the menu item as a series of unicode chars.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemPackedPermissions { get; init; } = string.Empty;

        /// <summary>
        /// A <see cref="List{T}"/> of string values. Each element contains one of the
        /// <see cref="AppPermissions"/> enum elements whose value is represented in the
        /// <see cref="MenuItemPackedPermissions"/> property.
        /// </summary>
        /// <remarks>
        /// This property is calculated by the consumer so as to prevent sending human-readable
        /// permission names across the wire.
        /// Typically some sort of extension method would be used used to convert the
        /// <see cref="MenuItemPackedPermissions"/> property. For this demo, the
        /// MenuItemDefinitionsQueryHandler will build this DTO.
        /// </remarks>
        public List<string> MenuItemRequiredPermissions { get; set; } = new();

        /// <summary>
        /// String value containing the CSS specifier for an icon displayed to the left of the
        /// menu item.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemIconCss { get; init; } = string.Empty;

        /// <summary>
        /// String value containing the identifier for the menu item's parent.
        /// This is the parent's <see cref="MenuItemId"/>.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemParentId { get; init; }

        /// <summary>
        /// Boolean value indicating if the menu item represents a separator.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public bool MenuItemIsSeparator { get; init; }

        /// <summary>
        /// String value containing the URL associated with the menu item.
        /// At this point, none of the menu systems use this property.
        /// Typically this is mapped to/from a persisted object (e.g., a MenuItemDefinition model).
        /// </summary>
        public string MenuItemUrl { get; init; } = string.Empty;
    }
}
