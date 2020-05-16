﻿// <copyright file="Dialog_InventoryOverview.cs" company="Zizhen Li">
// Copyright (c) 2019 - 2020 Zizhen Li. All rights reserved.
// Licensed under the LGPL-3.0-only license. See LICENSE.md file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using UnityEngine;
using Verse;

namespace AwesomeInventory.UI
{
    /// <summary>
    /// A dialog window of overview on inventory.
    /// </summary>
    public class Dialog_InventoryOverview : Window
    {
        private static List<OverviewTab> _tabs = new List<OverviewTab>();

        private static TipDisplayer _tipDisplayer = new TipDisplayer(
            new List<string>()
            {
                UIText.LoadoutTabTip1.TranslateSimple(),
                UIText.LoadoutTabTip2.TranslateSimple(),
            });

        private OverviewTab _activeTab = _tabs.First();

        static Dialog_InventoryOverview()
        {
            _tabs.Add(new LoadoutTab());
            _tabs.Add(new InventoryTab());
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dialog_InventoryOverview"/> class.
        /// </summary>
        public Dialog_InventoryOverview()
        {
            this.doCloseX = true;
            this.forcePause = true;
            this.absorbInputAroundWindow = true;
            this.closeOnClickedOutside = true;
            this.resizeable = true;
            this.draggable = true;
        }

        /// <summary>
        /// Gets the initial size for the dialog window.
        /// </summary>
        public override Vector2 InitialSize { get; } = new Vector2(GenUI.GetWidthCached(UIText.TenCharsString.Times(11)), Verse.UI.screenHeight / 2f);

        /// <summary>
        /// Called once before the window is opened.
        /// </summary>
        public override void PreOpen()
        {
            base.PreOpen();
            _tabs.ForEach(t => t.PreOpen());
        }

        /// <summary>
        /// Draw contents in <paramref name="inRect"/>.
        /// </summary>
        /// <param name="inRect"> Rect for drawing. </param>
        public override void DoWindowContents(Rect inRect)
        {
            WidgetRow widgetRow = new WidgetRow(inRect.x, inRect.y, UIDirection.RightThenDown);
            foreach (OverviewTab tab in _tabs)
            {
                if (widgetRow.ButtonText(tab.Label))
                {
                    _activeTab = tab;
                    _activeTab.PreSwitch();
                }
            }

            float rollingY = widgetRow.FinalY + GenUI.ListSpacing;
            Widgets.DrawLineHorizontal(inRect.x, rollingY, inRect.width);

            _activeTab.DoTabContent(inRect.ReplaceyMin(rollingY + GenUI.GapTiny).ReplaceyMax(inRect.yMax - GenUI.ListSpacing));

            widgetRow = new WidgetRow(inRect.x, inRect.yMax - GenUI.ListSpacing, UIDirection.RightThenDown);
            widgetRow.Label(UIText.Tips.Translate(_tipDisplayer.GetTip()));
        }
    }
}