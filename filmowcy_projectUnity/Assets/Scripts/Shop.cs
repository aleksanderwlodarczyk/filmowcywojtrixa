using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {

	public int Money
	{
		get
		{
			return _money;
		}
		set
		{
			_money = value;
			RefreshMoney();
		}
	}

	private int _money;

	public FirebaseFlow firebase;  // to set money and items
	public UserFlow user;  // to set money and items
	public Text cashText;

	private void Start()
	{
		Invoke("GetMoney", 3f);	
	}

	public int Price(int item)
	{
		switch (item)
		{
			case 1:
				return 10;
				break;
			case 2:
				return 20;
				break;
			case 3:
				return 40;
				break;
			default:
				return -1;
				break;
		}
	}

	public void BuyItem(int item, ShopButton button)
	{
		//check if you have enough money
		//update money here and in firebase
		//update items  in firebase
		switch (item)
		{
			case 1: // doubleJump cost 10

				if(Money >= 10 && !user.doubleJump)
				{
					firebase.SetUserItem(user.loggedUser, "doubleJump", true);
					Money -= 10;
					firebase.SetUserMoney(user.loggedUser, (-10), true);
					user.doubleJump = true;
					button.ChangeColor(ShopButtonColors.Bought);
				}

				break;

			case 2: // extendedBat cost 20

				if (Money >= 20 && !user.extendedBat)
				{
					firebase.SetUserItem(user.loggedUser, "extendedBattery", true);
					Money -= 20;
					firebase.SetUserMoney(user.loggedUser, (-20), true);
					user.extendedBat = true;
					button.ChangeColor(ShopButtonColors.Bought);

				}

				break;

			case 3: // oneHitShield cost 40

				if (Money >= 40 && !user.oneHitShield)
				{
					firebase.SetUserItem(user.loggedUser, "oneHitShield", true);
					Money -= 40;
					firebase.SetUserMoney(user.loggedUser, (-40), true);
					user.oneHitShield = true;
					button.ChangeColor(ShopButtonColors.Bought);

				}

				break;
		}
	}

	public void RefreshMoney()
	{
		cashText.text = Money.ToString();
	}

	private void GetMoney()
	{
		Money = user.cash;

		foreach(ShopButton shopButton in GameObject.FindObjectsOfType<ShopButton>())
		{
			shopButton.CheckPrice();
		}
	}
}
