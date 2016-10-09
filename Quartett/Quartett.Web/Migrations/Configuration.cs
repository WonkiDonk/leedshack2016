using System;
using System.Collections.Generic;
using System.Linq;
using Quartett.Web.Contexts;
using Quartett.Web.Contexts.Entities;

namespace Quartett.Web.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<Contexts.GameContext>
    {
        private static readonly Random Random = new Random();

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Contexts.GameContext context)
        {
            DeleteExistingData(context);
            GenerateCardCharacteristics(context);
        }

        private static void DeleteExistingData(Contexts.GameContext context)
        {
            var game = context.Games.FirstOrDefault();

            if (game != null)
            {
                game.PlayerCards.Clear();
                context.Games.Remove(game);
                context.SaveChanges();
            }
        }

        private static void GenerateCardCharacteristics(GameContext context)
        {
            var cards = context.Cards.ToArray();
            var characteristicTypes = context.CharacteristicTypes.ToArray();

            foreach (var card in cards)
            {
                GenerateCharacteristics(card, characteristicTypes);
                context.SaveChanges();
            }
        }

        private static void GenerateCharacteristics(Card card, IEnumerable<CharacteristicType> characteristicTypes)
        {
            card.Characteristics.Clear();

            foreach (var characteristicType in characteristicTypes)
            {
                card.Characteristics.Add(new Characteristic
                {
                    TypeId = characteristicType.Id,
                    Value = GetRandomValueForType(characteristicType.Name)
                });
            }
        }

        private static double GetRandomValueForType(string name)
        {
            double maximumValue;

            switch (name)
            {
                case "Field Size Rating":
                case "Air Pollution":
                case "Flood Risk":
                    maximumValue = Scales.Metres;
                    break;
                case "Education":
                case "Transportation":
                    maximumValue = Scales.Kilometres;
                    break;
                case "Health Care":
                case "Culture":
                case "Commerce":
                    maximumValue = Scales.TenKilometres;
                    break;
                default:
                    maximumValue = 100;
                    break;
            }

            return Random.NextDouble() * maximumValue;
        }

        private static class Scales
        {
            internal const int Metres = 10;
            internal const int TenKilometres = 10000;
            internal const int Kilometres = 1000;
        }
    }
}
