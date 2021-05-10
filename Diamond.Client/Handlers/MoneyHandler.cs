using System.Globalization;
using CitizenFX.Core;
using CitizenFX.Core.UI;

namespace Diamond.Client.Handlers
{
    public class MoneyHandler : BaseScript
    {
        [EventHandler("MoneyUpdated")]
        private void MoneyUpdate(int money)
        {
            if (MainClient.Character != null)
                MainClient.Character.Money = money;
        }

        [EventHandler("BankUpdated")]
        private void BankUpdated(int money)
        {
            if (MainClient.Character != null)
                MainClient.Character.Bank = money;
        }

        [EventHandler("DirtyMoneyUpdated")]
        private void DirtyMoneyUpdated(int money)
        {
            if (MainClient.Character != null)
                MainClient.Character.DirtyMoney = money;
        }

        [EventHandler("SalaryGiven")]
        private void OnSalaryGiven(int money)
        {
            Screen.ShowNotification($"You have been given your salary of ~g~${money:N}~s~.");
        }
    }
}