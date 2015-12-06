using GrandTheftAuto.GameFolder.Classes;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GrandTheftAuto.GameFolder.Components
{
    public class ComponentStats : DrawableGameComponent
    {
        private GameClass game;
        private StatsService statsService;
        private SkillView skillView;
        private CharacterService characterService;
        public ComponentStats(GameClass game, StatsService statsService, CharacterService characterService)
            : base(game)
        {
            this.game = game;
            this.statsService = statsService;
            this.characterService = characterService;
        }
        public ComponentStats(GameClass game, SkillView skillView, StatsService statsService, CharacterService characterService)
            : base(game)
        {
            this.game = game;
            this.statsService = statsService;
            this.skillView = skillView;
            this.characterService = characterService;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            if (game.SingleClick(Keys.Escape)) //Pauza
            {
                game.EGameState = EGameState.Pause;
                game.ComponentEnable(this, false);
                characterService.IsStatsRunning = false;
                Game.IsMouseVisible = true;
            }
            if (game.SingleClick(game.controlsList[(int)EKeys.C].Key) && statsService != null && skillView == null) //vypnutí statů
            {
                game.ComponentEnable(this, false);
                game.IsMouseVisible = false;
                characterService.IsStatsRunning = false;
            }
            else if (game.SingleClick(game.controlsList[(int)EKeys.T].Key) && skillView != null) //vypnutí talentu
            {
                game.ComponentEnable(this, false);
                game.IsMouseVisible = false;
                characterService.IsStatsRunning = false;
            }
            if (skillView != null)
                skillView.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            game.spriteBatch.Begin();
            DrawOrder = 5;
            if (statsService != null && skillView == null)
            {
                string text = "Level:" + characterService.Character.Level + "\n" +
                              "Vitality:" + characterService.Character.Vitality + "      Health:" + characterService.Character.Hp +
                              "\n" +
                              "Intelect: " + characterService.Character.Intelect + "      Energy:" + characterService.Character.Energy +
                              "\n" +
                              "Stamina:" + characterService.Character.Stamina + "      Hp Regen:" + characterService.Character.HpRegen +
                              "\n" +
                              "Spirit:" + characterService.Character.Spirit + "      Energy Regen:" +
                              characterService.Character.EnergyRegen +
                              "\n" +
                              "Agility:" + characterService.Character.Agility + "      Speed:" + characterService.Character.Speed + "\n";
                game.spriteBatch.Draw(game.spritPergamen[0],
                    new Vector2(game.graphics.PreferredBackBufferWidth / 2 - game.spritPergamen[0].Width / 2,
                        game.graphics.PreferredBackBufferHeight / 2 - game.spritPergamen[0].Height / 2), Color.White);
                game.spriteBatch.DrawString(game.smallFont, text,
                    new Vector2(game.graphics.PreferredBackBufferWidth / 2 - game.smallFont.MeasureString(text).X / 2,
                        game.graphics.PreferredBackBufferHeight / 2 - game.smallFont.MeasureString(text).Y / 2), Color.Red);
            }
            if (skillView != null)
            {
                skillView.DrawSkillTable();
            }
            game.spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
