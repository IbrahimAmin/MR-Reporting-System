﻿define(['services/dataservice', 'config'], function (dataservice, config) {
    var visit = function () {
        var self = this;

        self.id = ko.observable();
        self.agentId = ko.observable();
        self.orderTypeId = ko.observable();
        self.orderTo = ko.observable();
        self.orderDate = ko.observable();
        self.estimateDate = ko.observable();
        self.deliverdDate = ko.observable();
        self.subject = ko.observable();
        self.netTotal = ko.observable();
    };

    visit.prototype = {
        initialize: function () {
            this.id(0);
            this.agentId(0);
            this.orderTypeId(0);
            this.orderTo(0);
            this.netTotal(0);
            this.orderDate(moment().format('DD/MM/YYYY'));
            this.estimateDate(moment().format('DD/MM/YYYY'));
            this.deliverdDate(moment().format('DD/MM/YYYY'));
            this.subject('');
        },
        fill: function (data) {
            this.id(data.id);
            this.agentId(data.agentId);
            this.orderTypeId(data.orderTypeId);
            this.orderTo(data.orderTo);
            this.netTotal(data.netTotal);
            this.orderDate(moment(data.orderDate).format('DD/MM/YYYY'));
            this.deliverdDate(moment(data.deliverdDate).format('DD/MM/YYYY'));
            this.subject(data.subject);
            this.estimateDate(moment(data.estimateDate).format('DD/MM/YYYY'));

        },
        getServerObject: function () {
            return {
                id: this.id(),
                agentId: this.agentId(),
                orderTypeId: this.orderTypeId(),
                orderTo: this.orderTo(),
                orderDate: moment(this.orderDate(), 'DD/MM/YYYY').format(),
                deliverdDate: moment(this.deliverdDate(), 'DD/MM/YYYY').format(),
                estimateDate: moment(this.estimateDate(), 'DD/MM/YYYY').format(),
                subject: this.subject(),
                netTotal: this.netTotal()

            }
        }
    }

    var orderItemModel = function () {
        var self = this;

        self.id = ko.observable();
        self.orderId = ko.observable();
        self.description = ko.observable();
        self.itemCode = ko.observable();
        self.unitPrice = ko.observable();
        self.quantity = ko.observable();
        self.drugsId = ko.observable();
    };

    var orderItem = ko.observable(new orderItemModel());

    var orderObject = ko.observable();

    var agents = ko.observableArray([]);

    var types = ko.observableArray([]);

    var visitees = ko.observableArray([]);

    var isEdit = ko.observable();

    var drugs = ko.observableArray([]);

    var orderItemId = ko.observable();

    function attached() {
        $("#rootwizard").bootstrapWizard({
            'tabClass': 'nav nav-tabs pull-right',
            'onTabClick': function (tab, navigation, index) {
                return false;
            }
        });
    };

    var nextTab = function (obj, e) {

        var activatedTab = $("#rootwizard").find("ul li:has([data-toggle='tab']).active");
        var tabToActivateIndex = $("#rootwizard").find("ul li:has([data-toggle='tab'])").index(activatedTab) + 1;

        var isValid = $("#eForm").valid();

        if (tabToActivateIndex == 1) {
            if (isEdit()) {
                $(e.target).button('loading');
                dataservice.editOrders(orderObject().getServerObject()).success(function () {
                    $(e.target).button('reset');
                });
            } else {
                $(e.target).button('loading');
                dataservice.addOrders(orderObject().getServerObject()).success(function (data) {

                    orderItemId(data["id"]);

                    $(e.target).button('reset');
                });
            }

            orderItem(new orderItemModel());

        }
        if (tabToActivateIndex == 2) {

            orderItem(new orderItemModel());

            orderItem().orderId(orderItemId());
        }

        if (tabToActivateIndex == 3) {
            router.navigate("orders");

        }

        if (tabToActivateIndex > -1) {

            var tabToActivate = $("#rootwizard").find("li:has([data-toggle='tab'])" + ":eq(" + tabToActivateIndex + ") a");

            tabToActivate.tab("show");
        }

    };

    var previousTab = function () {

        var activatedTab = $("#rootwizard").find("ul li:has([data-toggle='tab']).active");
        var tabToActivateIndex = $("#rootwizard").find("ul li:has([data-toggle='tab'])").index(activatedTab) - 1;
        if (tabToActivateIndex > -1) {
            var tabToActivate = $("#rootwizard").find("li:has([data-toggle='tab'])" + ":eq(" + tabToActivateIndex + ") a");
            tabToActivate.tab("show");
        }
    };

    var visitsAddEditLabel = ko.observable();

    var activate = function (orderId) {

        visitees([]);

        orderObject(new visit());

        orderObject().initialize();

        orderItemId(orderId);

        orderObject().orderTypeId.subscribe(function (value) {
            var type = ko.utils.arrayFirst(types(), function (item) {
                return item.id === value;
            });

            if (type) {
                if (type.action === 1) {
                    dataservice.getDocotors().success(function (data) {
                        visitees(data);
                    });
                } else if (type.action === 2) {
                    dataservice.getPharmacies().success(function (data) {
                        visitees(data);
                    });
                } else {
                    dataservice.getHospitals().success(function (data) {
                        visitees(data);
                    });
                }
            }
        });

        orderItem().drugsId.subscribe(function (value) {
            if (orderItem().drugsId()) {

                orderItem().drugsId(orderItem().drugsId());

                dataservice.getDrugsById(undefined, orderItem().drugsId()).success(function (data) {
                    orderItem().unitPrice(data["price"]);
                    orderItem().itemCode(data["code"]);
                });
            }

        });


        isEdit((orderId !== 0) ? true : false);

        visitsAddEditLabel(config.language.orders[config.currentLanguage()] + ' - ' + config.language.goAdd[config.currentLanguage()]);

        if (isEdit()) {
            dataservice.getOrdersById(undefined, parseInt(orderId)).success(function (data) {
                orderObject().fill(data);
            });
            visitsAddEditLabel(config.language.orders[config.currentLanguage()] + ' - ' + config.language.goEdit[config.currentLanguage()]);
        }

        dataservice.getAccounts().success(function (data) {
            agents(data);
        });

        dataservice.getTypesForVisits().success(function (data) {
            types(data);
        });
    };

    var addVisit = function (obj, e) {
        $(e.target).button('loading');
        dataservice.addOrders(orderObject().getServerObject()).success(function (data) {

            orderItemId(data["id"]);

            $(e.target).button('reset');
        });
    }

    var editVisit = function (obj, e) {
        $(e.target).button('loading');
        dataservice.editOrders(orderObject().getServerObject()).success(function () {
            $(e.target).button('reset');
        });
    }

    var addItem = function (obj, e) {
        $(e.target).button('loading');

        dataservice.addOrdersItems(orderItem()).success(function () {

            $(e.target).button('reset');

            orderItem(new orderItemModel());

            orderItem().orderId(orderItemId());
        });
    }

    var vm = {
        isEdit: isEdit,
        drugs: drugs,
        orderObject: orderObject,
        activate: activate,
        addVisit: addVisit,
        editVisit: editVisit,
        language: config.language,
        currentLanguage: config.currentLanguage,
        visitsAddEditLabel: visitsAddEditLabel,
        agents: agents,
        types: types,
        visitees: visitees,
        addItem: addItem,
        previousTab: previousTab,
        nextTab: nextTab,
        orderItem: orderItem

    }

    return vm;
});