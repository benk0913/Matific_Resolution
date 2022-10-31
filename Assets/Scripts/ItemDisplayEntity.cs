using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class ItemDisplayEntity : MonoBehaviour
{
    [SerializeField]
    Image ItemSpriteImage;

    [System.NonSerialized]
    public string CurrentSkin;

    public string Type;

    GameObject CurrentSkinObject;

    public void SetInfo(Item item = null, string type = default)
    {
        if (!string.IsNullOrEmpty(type))
        {
            this.Type = type;
        }

        if (CurrentSkinObject != null)
        {
            Destroy(CurrentSkinObject);
            CurrentSkinObject = null;
        }

        if (item == null)
        {
            if(ItemSpriteImage != null)
            {
                ItemSpriteImage.sprite = null;
                ItemSpriteImage.color = Color.clear;
            }

            return;
        }
        
        CurrentSkin = item.Skin;

        Addressables.LoadAssetAsync<Object>(item.Skin).Completed += (AsyncOperationHandle<Object> handledObject) =>
        {
            if (handledObject.Result.GetType() == typeof(Sprite))
            {
                if (ItemSpriteImage != null)
                {
                    ItemSpriteImage.sprite = (Sprite)handledObject.Result;
                }
            }
            else if (handledObject.Result.GetType() == typeof(Texture2D))
            {
                Texture2D texture = (Texture2D)handledObject.Result;
                if (ItemSpriteImage != null)
                {
                    ItemSpriteImage.sprite = Sprite.Create(texture ,new Rect(0,0,texture.width,texture.height),new Vector3(0f,0f),100f);
                }
            }    
            else if (handledObject.Result.GetType() == typeof(GameObject))
            {
                GameObject handledGameObject = (GameObject)handledObject.Result;
                Vector3 prefabOffset = handledGameObject.transform.position;

                CurrentSkinObject = Instantiate(handledGameObject);
                
                CurrentSkinObject.transform.SetParent(transform);
                CurrentSkinObject.transform.localScale = Vector3.one;
                CurrentSkinObject.transform.position = transform.position;
                CurrentSkinObject.transform.localPosition = prefabOffset;
            }
        };

    }
}
