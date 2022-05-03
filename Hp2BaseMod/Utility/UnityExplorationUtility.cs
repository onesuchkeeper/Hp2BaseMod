// Hp2BaseMod 2022, By OneSuchKeeper

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Hp2BaseMod.Utility
{
    public static class UnityExplorationUtility
    {
        //RectTransform

        public static void LogHierarchy(Transform target)
        {
            for (int i = 0; i < target.childCount; i++)
            {
                var child = target.GetChild(i);
                if (child != null)
                {
                    ModInterface.Instance.LogLine(child.name);
                    ModInterface.Instance.IncreaseLogIndent();
                    LogHierarchy(child);
                    ModInterface.Instance.DecreaseLogIndent();
                }
            }
        }

        public static void LogHierarchy(GameObject target) => LogHierarchy(target, new List<Component>() { });

        private static void LogHierarchy(GameObject target, List<Component> excluded)
        {
            var components = target.GetComponents<Component>();

            if (components.Length == 0)
            {
                ModInterface.Instance.LogLine($"No Components");
            }
            else
            {
                foreach (var component in components)
                {
                    ModInterface.Instance.LogLine($"Type: {component.GetType().Name}, Name: {component.name}");

                    ModInterface.Instance.IncreaseLogIndent();
                    if (!excluded.Contains(component))
                    {
                        excluded.Add(component);
                        LogHierarchy(component.gameObject, excluded.ToList());
                    }
                    else
                    {
                        ModInterface.Instance.LogLine("Has already been logged, avoiding recursion");
                    }
                    ModInterface.Instance.DecreaseLogIndent();
                }
            }
        }
    }
}
