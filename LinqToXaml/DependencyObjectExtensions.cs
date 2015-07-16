using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace LinqToXaml
{
    public static class DependencyObjectExtensions
    {
        public static IEnumerable<DependencyObject> Children(this DependencyObject dependencyObject)
        {
            var childrenCount = VisualTreeHelper.GetChildrenCount(dependencyObject);
            for (var i = 0; i < childrenCount; i++)
            {
                yield return VisualTreeHelper.GetChild(dependencyObject, i);
            }
        }

        public static IEnumerable<DependencyObject> ChildrenAndSelf(this DependencyObject dependencyObject)
        {
            yield return dependencyObject;
            var enumerator = Children(dependencyObject).GetEnumerator();

            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        public static IEnumerable<DependencyObject> DescendantsAndSelf(this DependencyObject dependencyObject)
        {
            var stack = new Stack<DependencyObject>();
            stack.Push(dependencyObject);
            while (stack.Count > 0)
            {
                var pop = stack.Pop();
                yield return pop;
                var childrenCount = VisualTreeHelper.GetChildrenCount(pop);
                for (var j = 0; j < childrenCount; j++)
                    stack.Push(VisualTreeHelper.GetChild(pop, j));
            }
        }

        public static IEnumerable<DependencyObject> Descendants(this DependencyObject dependencyObject)
        {
            var stack = new Stack<DependencyObject>();
            stack.Push(dependencyObject);
            while (stack.Count > 0)
            {
                var pop = stack.Pop();
                var childrenCount = VisualTreeHelper.GetChildrenCount(pop);
                for (var j = 0; j < childrenCount; j++)
                {
                    var child = VisualTreeHelper.GetChild(pop, j);
                    stack.Push(child);
                    yield return child;
                }
            }
        }

        public static DependencyObject Prarent(this DependencyObject dependencyObject)
        {
            return VisualTreeHelper.GetParent(dependencyObject);
        }

        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject dependencyObject)
        {
            var stack = new Stack<DependencyObject>();
            stack.Push(dependencyObject);

            while (stack.Count > 0)
            {
                var pop = stack.Pop();
                var parent = VisualTreeHelper.GetParent(pop);
                if (parent != null)
                {
                    stack.Push(parent);
                    yield return parent;
                }
            }
        }

        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject dependencyObject)
        {
            yield return dependencyObject;
            var enumerator = Ancestors(dependencyObject).GetEnumerator();

            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }
    }
}
