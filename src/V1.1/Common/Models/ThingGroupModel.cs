﻿// <copyright file="ThingGroupModel.cs" company="Zizhen Li">
// Copyright (c) 2019 - 2020 Zizhen Li. All rights reserved.
// Licensed under the LGPL-3.0-only license. See LICENSE.md file in the project root for full license information.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RimWorld;
using Verse;

namespace AwesomeInventory
{
    /// <summary>
    /// A data model class that divides things into three groups: weapons, apparels and miscellaneous.
    /// </summary>
    public class ThingGroupModel
    {
        /// <summary>
        /// Gets or sets a list of weapons.
        /// </summary>
        public List<ThingWithComps> Weapons;

        /// <summary>
        /// Gets or sets a list of apparels.
        /// </summary>
        public List<Apparel> Apparels;

        /// <summary>
        /// Gets or sets a list of various items.
        /// </summary>
        public List<Thing> Miscellaneous;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThingGroupModel"/> class.
        /// </summary>
        /// <param name="weapons"> A list of weapons. </param>
        /// <param name="apparels"> A list of apparels. </param>
        /// <param name="miscellaneous"> A list of miscellaneous items. </param>
        public ThingGroupModel(List<ThingWithComps> weapons, List<Apparel> apparels, List<Thing> miscellaneous)
        {
            Weapons = weapons;
            Apparels = apparels.OrderByDescending(app => app.def.apparel.bodyPartGroups[0].listOrder).ToList();
            Miscellaneous = miscellaneous;
        }

        /// <summary>
        /// Gets a list of items which is sorted in the order of weapons, apparels and miscellaneous items.
        /// </summary>
        public List<Thing> OrderedList => Weapons.Cast<Thing>().Concat(this.Apparels).Concat(this.Miscellaneous).ToList();
    }
}
