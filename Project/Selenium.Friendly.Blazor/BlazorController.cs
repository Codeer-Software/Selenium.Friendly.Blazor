using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using Selenium.Friendly.Blazor.DotNetExecutor;
using System.Reflection;

namespace Selenium.Friendly.Blazor
{
    public class BlazorController
    {
        static TypeFinder typeFinder = new TypeFinder();

        public static ComponentBase FindComponentByType(string typeFullName)
        { 
            var list = GetComponents();
            return list.Where(x => x.GetType().FullName == typeFullName).FirstOrDefault();
        }

        static List<ComponentBase> GetComponents()
        {
            /*
            Microsoft.AspNetCore.Components.WebAssembly.Rendering.RendererRegistry
            が
            private static readonly Dictionary<int, WebAssemblyRenderer>? _renderers
            というフィールドを持っていて
            そこにApp以下Componentが入っている（今は）
            */

            var RendererRegistryType = typeFinder.GetType("Microsoft.AspNetCore.Components.WebAssembly.Rendering.RendererRegistry");
            var flgs = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            var _renderersField = RendererRegistryType.GetField("_renderers", flgs);

            dynamic _renderers = _renderersField!.GetValue(null);
            var list = new List<ComponentBase>();
            foreach (var e in _renderers!)
            {
                var valueField = e.GetType().GetProperty("Value", flgs);
                var obj = valueField.GetValue(e);
                if (obj == null) continue;
                GetDescendantsRendererLoop((object)obj, list);
            }
            return list;
        }

        static List<ComponentBase> GetDescendantsComponentLoop(ComponentBase parent, List<ComponentBase> list)
        {
            list.Add(parent);

            //今はこれで下位のコンポーネントを取ってこれるみたい
            /*
            foreach (var e in parent._renderHandle._renderer._componentStateById)
            {
                var child = e.ValueComponent;
            }
            */
            var flgs = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            var _renderHandleField = typeof(ComponentBase).GetField("_renderHandle", flgs);
            var _rendererFiled = typeof(RenderHandle).GetField("_renderer", flgs);
            var _renderHandle = _renderHandleField.GetValue(parent);
            var _renderer = _rendererFiled.GetValue(_renderHandle);
            GetDescendantsRendererLoop(_renderer, list);
            return list;
        }

        static void GetDescendantsRendererLoop(object _renderer, List<ComponentBase> list)
        {
            var flgs = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance;
            var _componentStateByIdField = typeof(Renderer).GetField("_componentStateById", flgs);
            dynamic _componentStateById = _componentStateByIdField.GetValue(_renderer);

            foreach (object e in _componentStateById)
            {
                var valueField = e.GetType().GetProperty("Value", flgs);
                var obj = valueField.GetValue(e);
                if (obj == null) continue;
                var prop = obj.GetType().GetProperty("Component", flgs);
                var val = prop.GetValue(obj);
                var child = val as ComponentBase;
                if (child == null) continue;

                if (list.Contains(child)) continue;
                GetDescendantsComponentLoop(child, list);
            }
        }
    }
}
