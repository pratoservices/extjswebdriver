Ext.application({
    name: 'Fiddle',
    launch: function () {
        //    Ext.Msg.alert('Fiddle', 'Welcome to Sencha Fiddle!');
        Ext.create('Ext.container.Viewport', {
            name: "viewPort2",
            layout: 'fit',
            items: [
                    {
                        xtype: 'textfield',
                        name: 'TestTextField',
                        fieldLabel: "Test Textfield"
                    }
            ]
        });
    }
});