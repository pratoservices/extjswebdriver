namespace ExtjsWd.Test.ExtSandBox
{
    public class ExtjsViewBoilerPlate
    {
        public static string AfterItems = @"]
                                                    });
                                                }
                                            });";

        public static string BeforeItems = @"Ext.application({
            name: 'Fiddle',
            launch: function()
                {
                    Ext.create('Ext.container.Viewport', {
                        name: 'viewPort2',
                        layout: 'fit',
                        items: [";
    }
}