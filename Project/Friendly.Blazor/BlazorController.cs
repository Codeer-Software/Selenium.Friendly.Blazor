using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Reflection;

namespace Friendly.Blazor
{
    public class BlazorController
    {
        static ComponentBase _app;

        public static void Initialize(ComponentBase app)
        {
            _app = app;
            JSInterface.FriendlyAccessEnabled = true;
        }

        public static ComponentBase FindComponentByType(string typeFullName)
        { 
            var list = new List<ComponentBase>();
            GetDescendants(_app, list);
            return list.Where(x => x.GetType().FullName == typeFullName).FirstOrDefault();
        }

        public static List<ComponentBase> GetDescendants(ComponentBase parent, List<ComponentBase> list)
        {
            list.Add(parent);

            //今はこれで下位のコンポーネントを取ってこれるみたい
            /*
            foreach (var e in parent._renderHandle._renderer._componentStateById)
            {
                var child = e.ValueComponent;
            }
            */

            var _renderHandleField = typeof(ComponentBase).GetField("_renderHandle", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var _rendererFiled = typeof(RenderHandle).GetField("_renderer", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
            var _componentStateByIdField = typeof(Renderer).GetField("_componentStateById", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);

            var _renderHandle = _renderHandleField.GetValue(parent);
            var _renderer = _rendererFiled.GetValue(_renderHandle);
            dynamic _componentStateById = _componentStateByIdField.GetValue(_renderer);
            foreach (object e in _componentStateById)
            {
                var valueField = e.GetType().GetProperty("Value", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                var obj = valueField.GetValue(e);
                if (obj == null) continue;
                var prop = obj.GetType().GetProperty("Component", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static | BindingFlags.Instance);
                var val = prop.GetValue(obj);
                var child = val as ComponentBase;
                if (child == null) continue;

                if (list.Contains(child)) continue;
                GetDescendants(child, list);
            }
            return list;
        }
    }
}
