﻿using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace Elinor
{
    internal class ClipboardTools
    {
        internal static void SetClipboardWrapper(double d)
        {
            bool copied = false;
            int Attempts = 0;

            while (copied == false && Attempts < 3)
            {
                try
                {
                    Clipboard.SetText(d > .01 ? Math.Round(d, 2).ToString(CultureInfo.InvariantCulture) : string.Empty);
                    copied = true;
                }
                catch (Exception)
                {
                    Attempts++;
                    Thread.Sleep(500 * Attempts);
                }
            }
        }

        internal static double GetSellPrice(double sell, Settings settings)
        {
            if (settings == null) return .0;

            double result = sell;

            if (settings.AdvancedStepSettings)
            {
                result -= (result*settings.SellPercentage > settings.SellThreshold)
                              ? settings.SellThreshold
                              : settings.SellPercentage*result;
            }
            else
            {
                result -= .01;
            }

            return result;
        }

        internal static double GetBuyPrice(double buy, Settings settings)
        {
            if (settings == null) return .0;

            double result = buy;

            if (settings.AdvancedStepSettings)
            {
                result += result*settings.BuyPercentage > settings.BuyThreshold
                              ? settings.BuyThreshold
                              : settings.BuyPercentage*result;
            }
            else
            {
                result += .01;
            }

            return result;
        }
    }
}