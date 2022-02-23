using System.Diagnostics;
using Selenium.Friendly.Blazor.Inside;
using Selenium.Friendly.Blazor.Inside.Protocol;

namespace Selenium.Friendly.Blazor
{
    public class BlazorAppFriend : AppFriend, IDisposable
	{
        int _appVarCreateCount;
        FriendlyConnectorCore _connector;

        protected override IFriendlyConnector FriendlyConnector { get { return new FriendlyConnectorWrap(this); } }

        public BlazorAppFriend(object webDriver)
		{
            ResourcesLocal.Initialize();

            _connector = new FriendlyConnectorCore(webDriver);

            //リソース初期化
            ResourcesLocal.Install(this);
        }

        public dynamic FindComponentByType(string typeFullName)
            => this.Type<BlazorController>().FindComponentByType(typeFullName);

        ~BlazorAppFriend()
		{
			Dispose(false);
		}

        public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

        protected virtual void Dispose(bool disposing)
		{
            //まあなんかきれいにするかな
            GC.Collect();
        }

        ReturnInfo SendAndReceive(ProtocolInfo info)
        {
            return _connector.SendAndReceive(info);
        }

        void AppVarCreateCountUp()
        {
            bool cleanUp = false;

            _appVarCreateCount++;

            //テスト中はGCの回収率が悪い時がある。
            //それに備えて、一定数AppVarを生成するごとにGCを実施するようにする。
            if (100 < _appVarCreateCount)
            {
                cleanUp = true;
                _appVarCreateCount = 0;
            }

            if (cleanUp)
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }

        class FriendlyConnectorWrap : IFriendlyConnector
        {
            BlazorAppFriend _app;
            public FriendlyConnectorWrap(BlazorAppFriend app)
            {
                _app = app;
            }

            public AppFriend App { get { return _app; } }

            public object Identity { get { return _app; } }

            public ReturnInfo SendAndReceive(ProtocolInfo info)
            {
                ReturnInfo ret = _app.SendAndReceive(info);
                if (ret != null && ((ret.ReturnValue as VarAddress) != null))
                {
                    _app.AppVarCreateCountUp();
                }
                return ret;
            }
        }
	}
}
