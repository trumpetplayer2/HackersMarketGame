using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class HackMinigame : MonoBehaviour
{
    InvestManager manager;
    float rigChance = 0;
    int attempts;
    public Scoreable answerTexts;
    public Scoreable questionTexts;
    public TextMeshPro attemptsLeft;
    Condition[] conditions = new Condition[3];

    //Load Minigame
    public void loadMinigame()
    {
        //Instantiate the manager
        manager = GameManager.instance.investManager;
        //Get the rig amount. This is unknown to the player, but is the reward for hacking
        rigChance = Random.Range(0.1f, 0.5f);
        //Generate Minigame
        GenerateMinigame();
    }

    public void GenerateMinigame()
    {
        //Reset Attempts
        attempts = 1;
        attemptsLeft.text = "Attempt: " + attempts;
        //Clear conditions
        conditions = new Condition[questionTexts.tmproTexts.Length];
        //Determine a correct answer
        string correctAnswer = randomString(true);
        string[] answers = new string[5];
        string[] rules = new string[conditions.Length]; ;
        answers[4] = correctAnswer;
        //Choose 3 rules
        for(int i = 0; i < conditions.Length; i++)
        {
            conditions[i] = chooseRule(i, correctAnswer);
            rules[i] = conditions[i].getText();
        }
        //Generate 4 random values
        for(int i = 0; i < 4; i++)
        {
            answers[i] = randomString(false);
        }
        //Shuffle Value List
        reshuffle(answers);
        //Update the scoreable script values
        answerTexts.updateTMP(answers);
        questionTexts.updateTMP(rules);
    }

    void reshuffle(string[] texts)
    {
        // Knuth shuffle algorithm
        for (int t = 0; t < texts.Length; t++)
        {
            string tmp = texts[t];
            int r = Random.Range(t, texts.Length);
            texts[t] = texts[r];
            texts[r] = tmp;
        }
    }

    string randomString(bool correct)
    {
        //All valid characters for puzzle
        string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        //Char list that will become the string
        char[] stringChars = new char[3];
        //Create string
        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[Random.Range(0, chars.Length)];
        }

        string temp = new string(stringChars);
        if (!correct)
        {
            int checkSum = 0;
            foreach(Condition c in conditions)
            {
                if (c.requirement(temp))
                {
                    checkSum += 1;
                }
            }
            //We want each incorrect answer to have at least 1 condition true, and 1 condition false
            if(checkSum == 0 || checkSum == conditions.Length)
            {
                //Invalid, return a different string
                return randomString(false);
            }
        }
        return temp;
    }

    public Condition chooseRule(int ruleNo, string correctAnswer)
    {
        Condition rule;
        int ruleNumber = Random.Range(0, 8);
        switch (ruleNumber)
        {
            case 0:
                rule = new Odd();
                break;
            case 1:
                rule = new Even();
                break;
            case 2:
                rule = new Vowel();
                break;
            case 3:
                rule = new Consonant();
                break;
            case 4:
                rule = new Numbers();
                break;
            case 5:
                rule = new Letter();
                break;
            case 6:
                rule = new MiddleValueOdd();
                break;
            case 7:
                rule = new Addition();
                break;
            default:
                rule = new Odd();
                break;
        }
        rule.initialize(correctAnswer);
        if(ruleNo > 0)
        {
            for (int i = 0; i < ruleNo + 1; i++)
            {
                if(rule == null) { continue; }
                if(conditions[i] == null) { continue; }
                if(rule.id() == conditions[i].id())
                {
                    //Rule of same type already chosen, get a new one
                    rule = chooseRule(ruleNo, correctAnswer);
                }
            }
        }
        return rule;
    }

    public void checkAnswer(TextMeshPro textmesh)
    {
        //-1 hour
        GameManager.instance.passTime(1);
        string input = textmesh.text;
        for(int i = 0; i < conditions.Length; i++)
        {
            //If the requirement is ever false, it is incorrect
            if (!conditions[i].requirement(input))
            {
                wrongAnswer();
                return;
            }
        }
        winMinigame();
    }

    public void wrongAnswer()
    {
        attemptsLeft.text = "Attempt: " + attempts;
        //3 Attempts, if failed 3 times, lose
        if (attempts < 3)
        {
            attempts += 1;
        }
        else
        {
            loseMinigame();
        }
    }

    public void winMinigame()
    {
        manager.skew += rigChance/attempts;
        //Restart
        loadMinigame();
    }

    public void loseMinigame()
    {
        manager.skew -= rigChance;
        //Restart
        loadMinigame();
    }

}
