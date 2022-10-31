﻿using System.Collections.Generic;
using UnityEngine;

public static class UserDataGetter
{
    public static UserData GetUserData()
    {
        return new UserData
        {
            Level = Random.Range(0, 8),
            Coins = Random.Range(50, 1500),
            ItemsOwned = new List<Item>(),
            Avatar = new AvatarData(),
        };
    }
}
