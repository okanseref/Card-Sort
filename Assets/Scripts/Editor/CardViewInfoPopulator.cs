using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using View.Card;

public class CardViewInfoPopulator
{
    private const string CardsFolderPath = "Assets/Sprites/Cards";
    private const string OutputSOPath = "Assets/Resources/Info/CardViewInfoCollection.asset";

    [MenuItem("Card Info Filler/Fill CardViewInfoCollection")]
    public static void PopulateCardViewInfoCollection()
    {
        // Load all sprites from the folder
        string[] guids = AssetDatabase.FindAssets("t:Sprite", new[] { CardsFolderPath });
        var cardViewInfos = new List<CardViewInfo>();

        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            Sprite sprite = AssetDatabase.LoadAssetAtPath<Sprite>(assetPath);

            if (sprite == null)
                continue;

            // Parse name, e.g., "Clubs2" or "DiamondsQueen"
            string name = sprite.name;
            if (TryParseCardInfo(name, out char suit, out int rank))
            {
                cardViewInfos.Add(new CardViewInfo
                {
                    Asset = sprite,
                    Suit = suit,
                    Rank = rank
                });
            }
            else
            {
                Debug.LogWarning($"Failed to parse card info from name: {name}");
            }
        }

        // Load or create ScriptableObject
        var collection = AssetDatabase.LoadAssetAtPath<CardViewInfoCollection>(OutputSOPath);
        if (collection == null)
        {
            collection = ScriptableObject.CreateInstance<CardViewInfoCollection>();
            AssetDatabase.CreateAsset(collection, OutputSOPath);
        }

        collection.CardViewInfos = cardViewInfos;
        EditorUtility.SetDirty(collection);
        AssetDatabase.SaveAssets();

        Debug.Log($"Populated {cardViewInfos.Count} CardViewInfo entries into collection.");
    }

    private static bool TryParseCardInfo(string name, out char suit, out int rank)
    {
        suit = '\0';
        rank = 0;

        string[] suits = { "Clubs", "Diamonds", "Hearts", "Spades" };
        Dictionary<string, int> faceCardRanks = new Dictionary<string, int>
        {
            { "Ace", 1 },
            { "Jack", 11 },
            { "Queen", 12 },
            { "King", 13 }
        };
        Dictionary<string, char> suitShort = new Dictionary<string, char>
        {
            { "Clubs", 'c' },
            { "Diamonds", 'd' },
            { "Hearts", 'h' },
            { "Spades", 's' }
        };

        foreach (var s in suits)
        {
            if (name.StartsWith(s))
            {
                suit = suitShort[s];
                string rankPart = name.Substring(s.Length);

                if (faceCardRanks.TryGetValue(rankPart, out rank))
                {
                    return true;
                }
                else if (int.TryParse(rankPart, out rank))
                {
                    return true;
                }
            }
        }

        return false;
    }
}
