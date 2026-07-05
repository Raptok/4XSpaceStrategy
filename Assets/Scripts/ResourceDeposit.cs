using System.Collections.Generic;

[System.Serializable]
public class ResourceDeposit
{
    public Dictionary<ResourceType, float> resources =
        new Dictionary<ResourceType, float>();

    public void Add(ResourceType type, float amount)
    {
        if (resources.ContainsKey(type))
            resources[type] += amount;
        else
            resources[type] = amount;
    }

    public float Get(ResourceType type)
    {
        return resources.ContainsKey(type) ? resources[type] : 0f;
    }
}