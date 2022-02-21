using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Unity implements Fuzzy Matching in 2021+. For now, this is our simplified version!
 */

public class FuzzyMatch : MonoBehaviour
{

    /*************** CheckContains(...) ***************/
    /* 
     * Given 2 strings of comma-seperated-values, a containsCSV and an inputCSV,
     * return TRUE IFF 
     * EXISTS a word in inputCSV 
     * which fuzzy-matches each word in containsCSV.
     */
    public static bool CheckContains(string containsCSV, string inputCSV, float matchThreshold)
    {

        string[] containsWords = containsCSV.Split(',');
        string[] inputWords = inputCSV.Split(',');

        foreach (string containsWord in containsWords)
        {
            bool hasMatch = false;
            foreach (string inputWord in inputWords) 
            {
                if (CalcDist(containsWord, inputWord) >= matchThreshold) {
                  hasMatch = true;
                }
            }
            if (!hasMatch) {
                return false;
            }
        }
        return true;
    }


    /*************** CalcDist(string str1, string str2) ***************/
    /* 
     * Given 2 strings, calculate their Levenshtein Distance Ratio.
     * C# implementation of algorithm described here: https://www.datacamp.com/community/tutorials/fuzzy-string-python
     * Ignores case.
     */
    public static float CalcDist(string strA, string strB) 
    {

        string str1 = strA.ToLower();
        string str2 = strB.ToLower();

        int rows = str1.Length + 1;
        int cols = str2.Length + 1;

        int[,] distance = new int[rows, cols];

        for (int i = 1; i < rows; i++) {
            for (int k = 1; k < cols; k++) {
                distance[i, 0] = i;
                distance[0, k] = k;
            }
        }

        for (int col = 1; col < cols; col++) {
            for (int row = 1; row < rows; row++) {
                
                int cost = 0; // cost of doing no substituions.
                if (str1[row - 1] != str2[col - 1]) {
                  cost = 2; // calculate cost of a substitution
                }
                distance[row, col] = Mathf.Min(
                  distance[row-1, col-1] + cost, 
                  distance[row-1, col] + 1,
                  distance[row, col-1] + 1
                );
            }
        }

        int lev_diff = (str1.Length + str2.Length) - distance[str1.Length, str2.Length];
        float lev_ratio = (float)lev_diff / (str1.Length + str2.Length);
        return lev_ratio;
    }
}