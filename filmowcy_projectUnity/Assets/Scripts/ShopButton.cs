using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour {

	private Shop shop;
	private UserFlow user;

	public int itemNum; 
	public Image buttonBackground;
	public Image priceBackgorund;

	private void Start()
	{
		shop = GameObject.FindObjectOfType<Shop>();
		user = shop.user;
	}

	public void ButtonClick()
	{
		shop.BuyItem(itemNum, this);
	}



	public void ChangeColor(Color color)
	{
		buttonBackground.color = color;
		priceBackgorund.color = color;
	}

	public void CheckPrice()
	{
		if (!(shop.Money >= shop.Price(itemNum))) ChangeColor(ShopButtonColors.Disabled);

		switch (itemNum)
		{
			case 1:
				if(user.doubleJump) ChangeColor(ShopButtonColors.Bought);
				break;
			case 2:
				if (user.extendedBat) ChangeColor(ShopButtonColors.Bought);
				break;
			case 3:
				if (user.oneHitShield) ChangeColor(ShopButtonColors.Bought);
				break;
			default:

				break;
		}
	}
}
