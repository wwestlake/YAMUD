using System;
using System.Collections.Generic;

// Define your AI actions here
public enum AIAction
{
    Idle,
    MoveTo,
    Attack,
    Flee
}

// Define your blackboard data structure
public class Blackboard
{
    public Dictionary<string, object> Data { get; private set; }

    public Blackboard()
    {
        Data = new Dictionary<string, object>();
    }

    public void SetValue(string key, object value)
    {
        Data[key] = value;
    }

    public T GetValue<T>(string key)
    {
        if (Data.TryGetValue(key, out object value))
        {
            return (T)value;
        }
        return default(T);
    }
}

// Define your AI behavior tree nodes
public abstract class BTNode
{
    public abstract AIAction Execute(Blackboard blackboard);
}

public class Selector : BTNode
{
    private readonly List<BTNode> children = new List<BTNode>();

    public void AddChild(BTNode child)
    {
        children.Add(child);
    }

    public override AIAction Execute(Blackboard blackboard)
    {
        foreach (var child in children)
        {
            AIAction result = child.Execute(blackboard);
            if (result != AIAction.Idle)
            {
                return result;
            }
        }
        return AIAction.Idle;
    }
}

public class Sequence : BTNode
{
    private readonly List<BTNode> children = new List<BTNode>();

    public void AddChild(BTNode child)
    {
        children.Add(child);
    }

    public override AIAction Execute(Blackboard blackboard)
    {
        foreach (var child in children)
        {
            AIAction result = child.Execute(blackboard);
            if (result == AIAction.Idle)
            {
                return AIAction.Idle;
            }
        }
        return AIAction.Idle;
    }
}

public class ActionNode : BTNode
{
    private readonly Func<Blackboard, AIAction> action;

    public ActionNode(Func<Blackboard, AIAction> action)
    {
        this.action = action;
    }

    public override AIAction Execute(Blackboard blackboard)
    {
        return action(blackboard);
    }
}

// Define your AI agent
public class AI
{
    private readonly Blackboard blackboard;
    private readonly BTNode behaviorTreeRoot;

    public AI(Blackboard blackboard, BTNode behaviorTreeRoot)
    {
        this.blackboard = blackboard;
        this.behaviorTreeRoot = behaviorTreeRoot;
    }

    public void Update()
    {
        AIAction action = behaviorTreeRoot.Execute(blackboard);
        Console.WriteLine("AI Action: " + action);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create blackboard
        var blackboard = new Blackboard();

        // Populate blackboard with shared information
        blackboard.SetValue("playerPosition", new Vector3(10, 0, 5));
        blackboard.SetValue("enemyPosition", new Vector3(12, 0, 8));

        // Create behavior tree
        var behaviorTreeRoot = new Selector();
        var sequence = new Sequence();
        sequence.AddChild(new ActionNode(MoveToPlayer));
        sequence.AddChild(new ActionNode(AttackPlayer));
        behaviorTreeRoot.AddChild(sequence);

        // Create AI agent
        var aiAgent = new AI(blackboard, behaviorTreeRoot);

        // Update AI agent
        aiAgent.Update();
    }

    static AIAction MoveToPlayer(Blackboard blackboard)
    {
        Vector3 playerPosition = blackboard.GetValue<Vector3>("playerPosition");
        // Implement move to player logic here
        return AIAction.MoveTo;
    }

    static AIAction AttackPlayer(Blackboard blackboard)
    {
        Vector3 enemyPosition = blackboard.GetValue<Vector3>("enemyPosition");
        // Implement attack player logic here
        return AIAction.Attack;
    }
}

// Dummy Vector3 class for demonstration purposes
public class Vector3
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Z { get; set; }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}
