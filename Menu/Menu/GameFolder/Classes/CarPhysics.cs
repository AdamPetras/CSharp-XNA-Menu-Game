using System;
using Menu.GameFolder.Interface;
using Microsoft.Xna.Framework;

namespace Menu.GameFolder.Classes
{
    public class CarPhysics : IPhysics
    {
        private const double BRAKEFRICTION = 0.3;               //koeficient tření při brzdění
        private const double GRAVITY = 9.81;                    //konstanta gravitace
        private const double COEFFCIENTOFRESISTANCE = 0.4;      //koeficient odporu auta
        private const double AREAOFTHECAR = 2*1.5;              //plocha auta zepředu
        private const double DENSITYOFENVIROMENT = 1.2;         //hustota vzduchu při 20°C
        private const double FLYWHEELRADIUS = 0.12;             //poloměr setrvačníku v metrech
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="eCar"></param>
        /// <param name="carWeight"></param>
        /// <param name="enginePower"></param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        public double Speed(ref double velocity, ECar eCar, double carWeight, double enginePower, GameTime gameTime)
        {
            if (eCar == ECar.Forward) //Maximální rychlost vpřed
            {
                velocity += AccelerationCalculate(velocity,carWeight, enginePower, gameTime);
            }
            else if (eCar == ECar.Backward && velocity <= 80) //Maximální rychlost vzad
                velocity += AccelerationCalculate(velocity,carWeight, enginePower, gameTime);
            return velocity;
        }
        /// <summary>
        /// Method to calculate acceleration of car
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="carWeight"></param>
        /// <param name="enginePower"></param>
        /// <param name="gameTime"></param>
        /// <returns></returns>
        private double AccelerationCalculate(double velocity, double carWeight, double enginePower, GameTime gameTime)
        {
            return ((enginePower-Resistanceforce(velocity))/carWeight)*(gameTime.ElapsedGameTime.TotalSeconds);     // ((Fmotor - Fodpor) / hmotnost auta) * čas
        }
        /// <summary>
        /// Method to calculate braking of car
        /// </summary>
        /// <param name="carWeight"></param>
        /// <returns></returns>
        private double BrakingCalculate(double carWeight)
        {
            return (BRAKEFRICTION*carWeight*GRAVITY)/carWeight;     // (koeficient brzdění * hmotnost auta * gravitace) / hmotnost auta
        }
        /// <summary>
        /// Method to calculate inertia of car
        /// </summary>
        /// <returns></returns>
        private double InertiaCalculate()
        {
            return 2*Math.PI*FLYWHEELRADIUS;       // setrvačnost auta 2 * Pi * poloměr setrvačníku
        }
        /// <summary>
        /// Method to calculate resistance of "air"
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        private double Resistanceforce(double velocity)
        {
            return 0.5*COEFFCIENTOFRESISTANCE*AREAOFTHECAR*DENSITYOFENVIROMENT*Math.Pow(velocity,2);       // 1/2 * koeficient odporu * plocha auta zepřed * hustota prostředí * rychlost na druhou
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        /// <returns></returns>
        public double Inertia(ref double velocity)
        {
            return velocity = velocity > 0 ? velocity - InertiaCalculate() : 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="velocity"></param>
        /// <param name="carWeight"></param>
        /// <returns></returns>
        public double Brake(ref double velocity, double carWeight)
        {
            return velocity = velocity > 0 ? velocity - BrakingCalculate(carWeight) : 0;
        }
    }
}
