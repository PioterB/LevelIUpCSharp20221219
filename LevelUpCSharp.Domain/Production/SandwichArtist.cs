﻿using System;
using System.Collections.Generic;
using System.Linq;
using LevelUpCSharp.Products;

namespace LevelUpCSharp.Production
{
	internal class SandwichArtist : ISandwichSetup, IGarnishable, ISossing, ISandwichDispatching
	{
		private readonly bool _butter;
		private readonly List<IGarnish> _ingredients = new List<IGarnish>();
		private ITopping _sos;
		private IKeyIngredient _keyIngredient;

		protected SandwichArtist(bool butter)
		{
			_butter = butter;
		}

		public static ISandwichSetup WithButter(bool butter)
		{
			return new SandwichArtist(butter);
		}

		/// <summary>
		/// Adds ingredient do the final product.
		/// </summary>
		/// <param name="ingredient"><see cref="IGarnish"/> added to the custom <see cref="Sandwich"/></param>
		/// <returns></returns>
		public IGarnishable AddVeg(IGarnish ingredient)
		{
			_ingredients.Add(ingredient);
			return this;
		}

		public ISandwichDispatching AddTopping(ITopping topping)
		{
			_sos = topping;
			return this;
		}

		public IGarnishable Use(IKeyIngredient ingredient)
		{
			_keyIngredient = ingredient;
			return this;
		}

		/// <summary>
		/// Materialize creation, gives <see cref="Sandwich"/> customized via creation API.
		/// </summary>
		/// <returns></returns>
		public Sandwich Wrap()
		{
			var ingredients = _ingredients.AsStrings().ToArray();
			return new Sandwich(
				_keyIngredient.Kind,
				DateTimeOffset.Now.AddDays(3),
				ingredients);
		}
	}
}
