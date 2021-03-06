﻿using System;
using System.Linq;
using Orchard.Commands;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Aspects;
using Orchard.Core.Common.Models;
using Orchard.Security;
using Orchard.Settings;
using Orchard.Widgets.Models;
using Orchard.Widgets.Services;

namespace Orchard.Widgets.Commands {
    public class WidgetCommands : DefaultOrchardCommandHandler {
        private readonly IWidgetsService _widgetsService;
        private readonly ISiteService _siteService;
        private readonly IMembershipService _membershipService;
        private const string LoremIpsum = "<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur a nibh ut tortor dapibus vestibulum. Aliquam vel sem nibh. Suspendisse vel condimentum tellus.</p>";

        public WidgetCommands(IWidgetsService widgetsService, ISiteService siteService, IMembershipService membershipService) {
            _widgetsService = widgetsService;
            _siteService = siteService;
            _membershipService = membershipService;
        }

        [OrchardSwitch]
        public string Title { get; set; }

        [OrchardSwitch]
        public string Zone { get; set; }

        [OrchardSwitch]
        public string Position { get; set; }

        [OrchardSwitch]
        public string Layer { get; set; }

        [OrchardSwitch]
        public string Identity { get; set; }

        [OrchardSwitch]
        public string Owner { get; set; }

        [OrchardSwitch]
        public string Text { get; set; }

        [OrchardSwitch]
        public bool UseLoremIpsumText { get; set; }

        [OrchardSwitch]
        public bool Publish { get; set; }

        [CommandName("widget create")]
        [CommandHelp("widget create <type> /Title:<title> /Zone:<zone> /Position:<position> /Layer:<layer> [/Identity:<identity>] [/Owner:<owner>] [/Text:<text>] [/UseLoremIpsumText:true|false]\r\n\t" + "Creates a new widget")]
        [OrchardSwitches("Title,Zone,Position,Layer,Identity,Owner,Text,UseLoremIpsumText")]
        public void Create(string type) {
            var widgetTypeNames = _widgetsService.GetWidgetTypeNames();
            if (!widgetTypeNames.Contains(type)) {
                throw new OrchardException(T("Creating widget failed : type {0} was not found. Supported widget types are: {1}.", 
                    type,
                    widgetTypeNames.Aggregate(String.Empty, (current, widgetType) => current + " " + widgetType)));
            }

            var layer = GetLayer(Layer);
            if (layer == null) {
                throw new OrchardException(T("Creating widget failed : layer {0} was not found.", Layer));
            }

            var widget = _widgetsService.CreateWidget(layer.ContentItem.Id, type, T(Title).Text, Position, Zone);
            var text = String.Empty;
            if (widget.Has<BodyPart>()) {
                if (UseLoremIpsumText) {
                    text = T(LoremIpsum).Text;
                }
                else {
                    if (!String.IsNullOrEmpty(Text)) {
                        text = Text;
                    }
                }
                widget.As<BodyPart>().Text = text;
            }
            if (String.IsNullOrEmpty(Owner)) {
                Owner = _siteService.GetSiteSettings().SuperUser;
            }
            var owner = _membershipService.GetUser(Owner);
            widget.As<ICommonPart>().Owner = owner;

            if (widget.Has<IdentityPart>() && !String.IsNullOrEmpty(Identity)) {
                widget.As<IdentityPart>().Identifier = Identity;
            }

            Context.Output.WriteLine(T("Widget created successfully.").Text);
        }

        private LayerPart GetLayer(string layer) {
            var layers = _widgetsService.GetLayers();
            return layers.FirstOrDefault(layerPart => String.Equals(layerPart.Name, layer, StringComparison.OrdinalIgnoreCase));
        }
    }
}