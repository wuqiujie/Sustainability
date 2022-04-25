using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCard : Card
{
    public int _conditionIndex;
    public int _conditionThresh;
    public int _changeIndex;
    public int _conditionTrue;
    public int _conditionFalse;

    //long term effect
    public int _Ltenvironment;
    public int _Ltlife;
    public int _Ltsocial;
    public int _Lteconomic;
    public int _Ltcost;

    public SpecialCard(int Id, string Card_name, string Card_description,
        int Cost, int Type, int Construction, Sprite Card_sprite,
       int Environment, int Life_expectancy,
       int Social_stability, int Economics, int[] goals, int[] nextCards, int isRoot,
       int conditionIndex, int conditionThresh, int changeIndex, int conditionTrue, int conditionFalse,
       int LTenvironment, int LTlife, int LTsocial, int LTeconomic, int LTcost)
    {
        id = Id;
        card_name = Card_name;
        card_description = Card_description;
        cost = Cost;
        type = Type;
        construction = Construction;
        card_sprite = Card_sprite;
        environment_index = Environment;
        life_expectancy_index = Life_expectancy;
        social_stability_index = Social_stability;
        economics_index = Economics;

        _goals = goals;
        _nextCards = nextCards;
        _isRoot = isRoot;

        _conditionIndex = conditionIndex;
        _conditionThresh = conditionThresh;
        _changeIndex = changeIndex;
        _conditionTrue = conditionTrue;
        _conditionFalse = conditionFalse;

        _Ltenvironment = LTenvironment;
        _Ltlife = LTlife;
        _Ltsocial = LTsocial;
        _Lteconomic = LTeconomic;
        _Ltcost = LTcost;
    }
    public override int getEnvironment()
    {
        if(_changeIndex == 0)
        {
            switch (_conditionIndex)
            {
                case 0:
                    if (GameManager.total_environment >= _conditionThresh)
                    {
                        environment_index = _conditionTrue;
                        return environment_index;
                    }
                    else
                    {
                        environment_index = _conditionFalse;
                        return environment_index;
                    }
                case 1:
                    if (GameManager.total_life >= _conditionThresh)
                    {
                        environment_index = _conditionTrue;
                        return environment_index;
                    }
                    else
                    {
                        environment_index = _conditionFalse;
                        return environment_index;
                    }
                case 2:
                    if (GameManager.total_social_stability >= _conditionThresh)
                    {
                        environment_index = _conditionTrue;
                        return environment_index;
                    }
                    else
                    {
                        environment_index = _conditionFalse;
                        return environment_index;
                    }
                case 3:
                    if (GameManager.total_economics >= _conditionThresh)
                    {
                        environment_index = _conditionTrue;
                        return environment_index;
                    }
                    else
                    {
                        environment_index = _conditionFalse;
                        return environment_index;
                    }
                default:
                    return environment_index;
            }
        }
        return environment_index;
    }
    public override int getLife_expectancy()
    {
        if (_changeIndex == 1)
        {
            switch (_conditionIndex)
            {
                case 0:
                    if (GameManager.total_environment >= _conditionThresh)
                    {
                        life_expectancy_index = _conditionTrue;
                        return life_expectancy_index;
                    }
                    else
                    {
                        life_expectancy_index = _conditionFalse;
                        return life_expectancy_index;
                    }
                case 1:
                    if (GameManager.total_life >= _conditionThresh)
                    {
                        life_expectancy_index = _conditionTrue;
                        return life_expectancy_index;
                    }
                    else
                    {
                        life_expectancy_index = _conditionFalse;
                        return life_expectancy_index;
                    }
                case 2:
                    if (GameManager.total_social_stability >= _conditionThresh)
                    {
                        life_expectancy_index = _conditionTrue;
                        return life_expectancy_index;
                    }
                    else
                    {
                        life_expectancy_index = _conditionFalse;
                        return life_expectancy_index;
                    }
                case 3:
                    if (GameManager.total_economics >= _conditionThresh)
                    {
                        life_expectancy_index = _conditionTrue;
                        return life_expectancy_index;
                    }
                    else
                    {
                        life_expectancy_index = _conditionFalse;
                        return life_expectancy_index;
                    }
                default:
                    return life_expectancy_index;
            }
        }
        return life_expectancy_index;
    }
    public override int getSocial_stability()
    {
        if (_changeIndex == 2)
        {
            switch (_conditionIndex)
            {
                case 0:
                    if (GameManager.total_environment >= _conditionThresh)
                    {
                        social_stability_index = _conditionTrue;
                        return social_stability_index;
                    }
                    else
                    {
                        social_stability_index = _conditionFalse;
                        return social_stability_index;
                    }
                case 1:
                    if (GameManager.total_life >= _conditionThresh)
                    {
                        social_stability_index = _conditionTrue;
                        return social_stability_index;
                    }
                    else
                    {
                        social_stability_index = _conditionFalse;
                        return social_stability_index;
                    }
                case 2:
                    if (GameManager.total_social_stability >= _conditionThresh)
                    {
                        social_stability_index = _conditionTrue;
                        return social_stability_index;
                    }
                    else
                    {
                        social_stability_index = _conditionFalse;
                        return social_stability_index;
                    }
                case 3:
                    if (GameManager.total_economics >= _conditionThresh)
                    {
                        social_stability_index = _conditionTrue;
                        return social_stability_index;
                    }
                    else
                    {
                        social_stability_index = _conditionFalse;
                        return social_stability_index;
                    }
                default:
                    return social_stability_index;
            }
        }
        return social_stability_index;
    }
    public override int getEconomics()
    {
        if (_changeIndex == 3)
        {
            switch (_conditionIndex)
            {
                case 0:
                    if (GameManager.total_environment >= _conditionThresh)
                    {
                        economics_index = _conditionTrue;
                        return economics_index;
                    }
                    else
                    {
                        economics_index = _conditionFalse;
                        return economics_index;
                    }
                case 1:
                    if (GameManager.total_life >= _conditionThresh)
                    {
                        economics_index = _conditionTrue;
                        return economics_index;
                    }
                    else
                    {
                        economics_index = _conditionFalse;
                        return economics_index;
                    }
                case 2:
                    if (GameManager.total_social_stability >= _conditionThresh)
                    {
                        economics_index = _conditionTrue;
                        return economics_index;
                    }
                    else
                    {
                        economics_index = _conditionFalse;
                        return economics_index;
                    }
                case 3:
                    if (GameManager.total_economics >= _conditionThresh)
                    {
                        economics_index = _conditionTrue;
                        return economics_index;
                    }
                    else
                    {
                        economics_index = _conditionFalse;
                        return economics_index;
                    }
                default:
                    return economics_index;
            }
        }
        return economics_index;
    }
    public int getLTEnvironment()
    {
        return _Ltenvironment;
    }
    public int getLTLife()
    {
        return _Ltlife;
    }
    public int getLTSocial()
    {
        return _Ltsocial;
    }
    public int getLTEconomic()
    {
        return _Lteconomic;
    }
    public int getLTCost()
    {
        return _Ltcost;
    }
}
