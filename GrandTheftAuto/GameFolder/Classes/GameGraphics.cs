using System;
using GrandTheftAuto.MenuFolder;
using Microsoft.Xna.Framework;

namespace GrandTheftAuto.GameFolder.Classes
{
    public class GameGraphics
    {
        public GraphicsService graphicsService { get; private set; }
        private const float Devadesat = (float)(Math.PI / 2);
        private const float Stoosmdesat = (float)Math.PI;
        private const float Dvestesedmdesat = (float)(Math.PI *1.5);
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="game"></param>
        public GameGraphics(GameClass game)
        {
            graphicsService = new GraphicsService(game);
            #region Road
            //Vlachovská ulice
            graphicsService.AddGraphics(new Vector2(0, 300), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(300, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(600, 300), game.spritTCross, Devadesat);
            graphicsService.AddGraphics(new Vector2(900, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1200, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1500, 300), game.spritCrossRoad, Devadesat);//X
            graphicsService.AddGraphics(new Vector2(1500, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1800, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1500, 600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1800, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1500, 1200), game.spritCrossRoad);//X
            graphicsService.AddGraphics(new Vector2(1500, 1500), game.spritRoad);

            graphicsService.AddGraphics(new Vector2(600, 50), game.spritHouse[2], "SMITHY", game.bigFont, colision: true);
            graphicsService.AddGraphics(new Vector2(-900, 1200), game.spritHouse[0], "Falric's Home", game.bigFont, colision: true);
            graphicsService.AddGraphics(new Vector2(-2700, 1800), game.spritHouse[1], "Garr's Home", game.bigFont, colision: true);
            //Vrběcká ulice
            graphicsService.AddGraphics(new Vector2(1200, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(900, 1200), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(600, 1200), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(600, 900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(300, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(0, 600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 1200), game.spritCrossRoad);//X
            graphicsService.AddGraphics(new Vector2(900, 1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(900, 1800), game.spritRoad);
            //Slavičínská Ulice            
            graphicsService.AddGraphics(new Vector2(1500, 1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 2100), game.spritCrossRoad, Devadesat);//X
            graphicsService.AddGraphics(new Vector2(1200, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(900, 2100), game.spritCrossRoad);
            graphicsService.AddGraphics(new Vector2(600, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(300, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(0, 2100), game.spritCrossRoad);//X
            graphicsService.AddGraphics(new Vector2(0, 1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(300, 1800), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(600, 1800), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(1200, 1800), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(900, 2400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(900, 2700), game.spritDeadEndRoad, Stoosmdesat);
            //Vlachovice park
            graphicsService.AddGraphics(new Vector2(-300, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(0, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(300, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(600, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(900, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(1200, 0), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(-300, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(0, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(300, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(600, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(900, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(1200, -300), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(-300, -600), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(0, -600), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(300, -600), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(600, -600), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(900, -600), game.spritGrass);
            //Kloboucká ulice
            graphicsService.AddGraphics(new Vector2(1500, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, -600), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(1200, -600), game.spritRoadCurveRight, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(900, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1200, -900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1200, -1200), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(900, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(600, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(300, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(0, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-300, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, -1200), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-600, -900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, -600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, 300), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(-300, 300), game.spritRoad, Devadesat);
            //Křekovská ulice
            graphicsService.AddGraphics(new Vector2(-600, -1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, -1800), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-900, -1800), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1200, -1800), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1500, -1800), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(-1500, -1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, -1200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, -900), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(-1500, -600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 300), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-1200, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-900, 300), game.spritRoad, Devadesat);
            //Křekov park
            graphicsService.AddGraphics(new Vector2(-1200, 0), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(-1200, -300), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(-1200, -600), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(-1200, -900), game.spritGrass);
            graphicsService.AddGraphics(new Vector2(-1200, -1200), game.spritTree, colision: true);
            #region Zlínská ulice
            graphicsService.AddGraphics(new Vector2(1500, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, -1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2100, -1200), game.spritRoadCurveLeft, Devadesat);
            graphicsService.AddGraphics(new Vector2(2100, -1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2100, -1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2100, -2100), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(2400, -2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2700, -2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(3000, -2100), game.spritRoadCurveRight, Devadesat);
            graphicsService.AddGraphics(new Vector2(3000, -1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, -1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, -1200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, -900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, -600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 300), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(2700, 300), game.spritRoad, Devadesat);
            //Slepá
            graphicsService.AddGraphics(new Vector2(2400, 300), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(2400, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, -600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, -900), game.spritDeadEndRoad);

            graphicsService.AddGraphics(new Vector2(2100, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(3000, 600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 1200), game.spritTCross, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2700, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2100, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(3000, 1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 2100), game.spritTCross, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2700, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2100, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 2100), game.spritRoad, Devadesat);
            #endregion
            #region Ostravská ulice
            graphicsService.AddGraphics(new Vector2(-1800, -1800), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2100, -1800), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(-2100, -1500), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-2400, -1500), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2700, -1500), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-3000, -1500), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(-3000, -1200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, -900), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-2700, -900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, -900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2100, -900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, -900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-3000, -600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, -300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 0), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 600), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-2700, 600), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, 600), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2100, 600), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 600), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-1800, 900), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-1500, 900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1200, 900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-900, 900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, 900), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-600, 1200), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-300, 1200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 1200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1800, 1500), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-1500, 1500), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1200, 1500), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-1200, 1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1200, 2100), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(-900, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-300, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1500, 2100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 2100), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(-1800, 2400), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-2100, 2400), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, 2400), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2700, 2400), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-3000, 2400), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-3000, 2100), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 1800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 1500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 1200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 900), game.spritRoad);
            #endregion
            #region Brněnská ulice
            graphicsService.AddGraphics(new Vector2(2700, 3000), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 3000), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(2100, 3000), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(2100, 3300), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(1800, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 3900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2100, 3900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 3900), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(2400, 3600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, 3300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2700, 3900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2700, 3900), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 5100), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(1800, 5400), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2100, 5400), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 5400), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(2700, 5400), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 5100), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(2400, 4500), game.spritDeadEndRoad);
            graphicsService.AddGraphics(new Vector2(1800, 6000), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(1800, 6300), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2100, 6300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 6300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2700, 6300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1800, 7500), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(1800, 7200), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(2100, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 7200), game.spritTCross, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2400, 7500), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(2700, 7500), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(2400, 6900), game.spritDeadEndRoad);
            //prava cesta dolu
            graphicsService.AddGraphics(new Vector2(3000, 2400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 2700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 3000), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(3000, 3300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 3600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 3900), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(3000, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 5100), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 5400), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(3000, 5700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 6000), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 6300), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(3000, 6600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 6900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 7200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(3000, 7500), game.spritRoadCurveRight, Stoosmdesat);
            //leva cesta dolu
            graphicsService.AddGraphics(new Vector2(1500, 3300), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(1500, 3000), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 2700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 2400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 3600), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(1500, 3900), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(1500, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 5100), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(1500, 5400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 5700), game.spritTCross, Stoosmdesat);//T
            graphicsService.AddGraphics(new Vector2(1500, 6000), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(1500, 6300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 6600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 6900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 7200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(1500, 7500), game.spritTCross, Dvestesedmdesat);//T
            #endregion
            #region Rokytenská ulice
            //Ulice nahoře
            graphicsService.AddGraphics(new Vector2(300, 3000), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(300, 3300), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(600, 3300), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(900, 3300), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(900, 3600), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(1200, 3600), game.spritRoad, Devadesat);
            //Slepá
            graphicsService.AddGraphics(new Vector2(600, 3600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 3900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(600, 5100), game.spritDeadEndRoad, Stoosmdesat);
            //Ulice dolu
            graphicsService.AddGraphics(new Vector2(600, 6000), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(600, 5700), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(900, 5700), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(1200, 5700), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(600, 6600), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(900, 6600), game.spritDeadEndRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(0, 6600), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-300, 6600), game.spritDeadEndRoad, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(600, 7500), game.spritRoad, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(900, 7500), game.spritRoad, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(1200, 7500), game.spritRoad, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(0, 7500), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-300, 7500), game.spritRoadCurveLeft, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-300, 7200), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-600, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-900, 7200), game.spritRoad, Devadesat);
            //Cesta dolů
            graphicsService.AddGraphics(new Vector2(0, 2400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 2700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 3000), game.spritCrossRoad);
            graphicsService.AddGraphics(new Vector2(0, 3300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 3600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 3900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 5100), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 5400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 5700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(0, 6000), game.spritTCross, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(300, 6000), game.spritTCross, Devadesat);
            graphicsService.AddGraphics(new Vector2(300, 6300), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(300, 6600), game.spritCrossRoad);
            graphicsService.AddGraphics(new Vector2(300, 6900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(300, 7200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(300, 7500), game.spritTCross, Dvestesedmdesat);
            #endregion
            #region Bojkovská ulice
            //Ulice nahoře
            graphicsService.AddGraphics(new Vector2(-2700, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2100, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 3300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1500, 3300), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(-1200, 3300), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-1200, 3000), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(-900, 3000), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, 3000), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-300, 3000), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1500, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 3900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1500, 3600), game.spritRoad);
            //Hlavní cesta
            graphicsService.AddGraphics(new Vector2(-3000, 2700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 3000), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 3300), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-3000, 3600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 3900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 4200), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 4500), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 5100), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-3000, 5400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-3000, 5700), game.spritDeadEndRoad, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-2700, 5100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, 5100), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(-2100, 5100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 5100), game.spritTCross, Devadesat);//T
            graphicsService.AddGraphics(new Vector2(-1500, 5100), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(-1200, 5100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-900, 5100), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, 5100), game.spritRoadCurveLeft);
            graphicsService.AddGraphics(new Vector2(-600, 5400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, 5700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-600, 6000), game.spritTCross);//T
            graphicsService.AddGraphics(new Vector2(-300, 6000), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-600, 6300), game.spritRoadCurveRight, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-900, 6300), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1200, 6300), game.spritRoadCurveRight);
            graphicsService.AddGraphics(new Vector2(-1200, 6600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1200, 6900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1200, 7200), game.spritTCross, Dvestesedmdesat);//T
            graphicsService.AddGraphics(new Vector2(-1500, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-1800, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2100, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-2400, 7200), game.spritTCross, Dvestesedmdesat);//T
            //Slepé dolů
            graphicsService.AddGraphics(new Vector2(-2700, 7200), game.spritRoad, Devadesat);
            graphicsService.AddGraphics(new Vector2(-3000, 7200), game.spritDeadEndRoad, Dvestesedmdesat);
            graphicsService.AddGraphics(new Vector2(-2400, 6900), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-2400, 6600), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-2400, 6300), game.spritDeadEndRoad);
            //Slepé nahoře
            graphicsService.AddGraphics(new Vector2(-1800, 5400), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1800, 5700), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-1800, 6000), game.spritDeadEndRoad, Stoosmdesat);
            graphicsService.AddGraphics(new Vector2(-2400, 4800), game.spritRoad);
            graphicsService.AddGraphics(new Vector2(-2400, 4500), game.spritDeadEndRoad);

            #endregion
            #endregion
            #region ColisionGraphics
            //Vlachovice
            graphicsService.AddGraphics(new Vector2(300, 600), game.spritHouse[1], colision: true);
            graphicsService.AddGraphics(new Vector2(900, 600), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(1200, 600), game.spritHouse[1], Devadesat, true);
            //Vrbětice
            graphicsService.AddGraphics(new Vector2(300, 900), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(1200, 900), game.spritHouse[1], Stoosmdesat, true);
            graphicsService.AddGraphics(new Vector2(900, 900), game.spritTree, colision: true);
            //Slavičín
            graphicsService.AddGraphics(new Vector2(1200, 1500), game.spritHouse[0], Devadesat, true);
            graphicsService.AddGraphics(new Vector2(600, 1500), game.spritHouse[0], Devadesat, true);
            graphicsService.AddGraphics(new Vector2(300, 1500), game.spritTree, colision: true);
            //Klobouky nad parkem
            graphicsService.AddGraphics(new Vector2(900, -900), game.spritHouse[0], Devadesat, true);
            graphicsService.AddGraphics(new Vector2(600, -900), game.spritHouse[1], Devadesat, true);
            graphicsService.AddGraphics(new Vector2(300, -900), game.spritTree, colision: true);
            graphicsService.AddGraphics(new Vector2(0, -900), game.spritHouse[0], Devadesat, true);
            graphicsService.AddGraphics(new Vector2(-300, -900), game.spritHouse[1], colision: true);
            //Klobouky na pravo
            graphicsService.AddGraphics(new Vector2(1500, -900), game.spritHouse[1], colision: true);
            graphicsService.AddGraphics(new Vector2(1800, -600), game.spritHouse[1], colision: true);
            graphicsService.AddGraphics(new Vector2(1800, -300), game.spritHouse[0], colision: true);
            graphicsService.AddGraphics(new Vector2(1800, 0), game.spritHouse[0], colision: true);
            //Klobouky na levo
            graphicsService.AddGraphics(new Vector2(-900, 0), game.spritHouse[0], colision: true);
            graphicsService.AddGraphics(new Vector2(-900, -300), game.spritHouse[0], colision: true);
            graphicsService.AddGraphics(new Vector2(-900, -600), game.spritHouse[1], Stoosmdesat, true);
            graphicsService.AddGraphics(new Vector2(-900, -900), game.spritHouse[1], Stoosmdesat, true);
            graphicsService.AddGraphics(new Vector2(-900, -1200), game.spritHouse[0], colision: true);
            #endregion
        }
        /// <summary>
        /// Method to detect simple(rectangle) Colision
        /// </summary>
        public bool Colision(Rectangle colisionRectangle)
        {
            for (int i = 0; i < graphicsService.ColisionList().Count; i++)
            {
                if (graphicsService.ColisionList()[i].Intersects(colisionRectangle))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Method to draw objects
        /// </summary>
        public void DrawColiadableGraphics()
        {
            graphicsService.DrawColiadableGraphics();
        }

        public void DrawNonColiadableGraphics()
        {
            graphicsService.DrawNonColiadableGraphics();
        }
    }
}