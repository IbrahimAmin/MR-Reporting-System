using System.Collections.Generic;
using System.Linq;
using MR_Reporting_System_Data_Context.Context;
using MR_Reporting_System_Interface.IDataService;
using MR_Reporting_System_Model.DataTransferObjectModel;
using System;

namespace MR_Reporting_System_Data_Service.Repository
{
    public class OrdersRepository : GenericRepository<MedicalTechnoEntities, Order>, IOrdersRepository
    {

        public List<DtoOrders> selectAll(string lang)
        {
            var list = new List<DtoOrders>();
 
            list = (from q in Context.Orders.Include("agent")
                    where q.deletedBy == null
                    select new DtoOrders
                    {
                        id = q.id,
                        orderTo = q.orderTo,
                        orderTypeId = q.orderTypeId,
                        agentName = q.Agent.ContactName,
                        agentId = q.agentId,
                        subject = q.subject,
                        orderDate = q.orderDate,
                        estimateDate = q.estimateDate,
                        deliverdDate = q.deliverdDate,
                        supervisorApprove = q.supervisorApprove,
                        deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                        supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                        isDeliverd = q.isDeliverd,
                        supervisorDate = q.supervisorDate,
                        noOfItems = q.noOfItems,
                        total = q.total,
                        netTotal = q.netTotal,
                        lastEditBy = q.lastEditBy,
                        lastEditName = q.Agent2.ContactName,
                        lastEditDate = q.lastEditDate
                    }).ToList();
             
            foreach (DtoOrders item in list)
            {
                var obj = Context.DefaultLists.FirstOrDefault(x => x.Id == item.orderTypeId).Action;

                switch (obj)
                {
                    case 1:

                        item.orderTypeName = "Doctors";
                        item.clientName = Context.Docotors.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 2:
                        item.orderTypeName = "Pharmacies";
                        item.clientName = Context.Pharmacies.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 3:
                        item.orderTypeName = "Hospitals";
                        item.clientName = Context.Hospitals.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 4:
                        item.orderTypeName = "Distributers";
                        item.clientName = Context.Distributers.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                }
            }
            return list;
        }
        public List<DtoOrders> selectBySupervisor(int supervisorId)
        {
            var list = new List<DtoOrders>();

            list = (from q in Context.Orders
                    where q.deletedBy == null && q.supervisorApprove == null
                    && (Context.Agents.Where(x => x.SupervisorId == supervisorId).Select(x => x.id).ToList()).Contains((int)q.agentId)
                    select new DtoOrders
                    {
                        id = q.id,
                        orderTo = q.orderTo,
                        orderTypeId = q.orderTypeId,
                        agentId = q.agentId,
                        agentName = q.Agent1.ContactName,
                        subject = q.subject,
                        orderDate = q.orderDate,
                        estimateDate = q.estimateDate,
                        deliverdDate = q.deliverdDate,
                        supervisorApprove = q.supervisorApprove,
                        deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                        supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                        isDeliverd = q.isDeliverd,
                        supervisorDate = q.supervisorDate,
                        noOfItems = q.noOfItems,
                        total = q.total,
                        netTotal = q.netTotal,
                        lastEditBy = q.lastEditBy,
                        lastEditName = q.Agent2.ContactName,
                        lastEditDate = q.lastEditDate
                    }).ToList();

            foreach (DtoOrders item in list)
            {
                var obj = Context.DefaultLists.FirstOrDefault(x => x.Id == item.orderTypeId).Action;

                switch (obj)
                {
                    case 1:

                        item.orderTypeName = "Doctors";
                        item.clientName = Context.Docotors.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 2:
                        item.orderTypeName = "Pharmacies";
                        item.clientName = Context.Pharmacies.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 3:
                        item.orderTypeName = "Hospitals";
                        item.clientName = Context.Hospitals.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                    case 4:
                        item.orderTypeName = "Distributers";
                        item.clientName = Context.Distributers.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        break;
                }
            }
            return list;
        }

        public List<DtoOrders> getOrdersByClient(int clientId, string lang)
        {
            var list = new List<DtoOrders>();
            if (lang == "en")
            {
                list = (from q in Context.Orders
                        where q.deletedBy == null && q.orderTo == clientId
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentId = q.agentId,
                            agentName = q.Agent1.ContactName,
                            subject = q.subject,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            noOfItems = q.noOfItems,
                            deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                            supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditName = q.Agent2.ContactName,
                            lastEditDate = q.lastEditDate
                        }).ToList();
            }
            else
            {
                list = (from q in Context.Orders
                        where q.deletedBy == null && q.orderTo == clientId
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentName = q.Agent1.ContactName,
                            agentId = q.agentId,
                            subject = q.subject,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                            supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                            noOfItems = q.noOfItems,
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditName = q.Agent2.ContactName,
                            lastEditDate = q.lastEditDate
                        }).ToList();
            }
            foreach (DtoOrders item in list)
            {
                var obj = Context.DefaultLists.FirstOrDefault(x => x.Id == item.orderTypeId).Action;

                switch (obj)
                {
                    case 1:
                        item.clientName = Context.Docotors.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Doctors";
                        break;
                    case 2:
                        item.clientName = Context.Pharmacies.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Pharmacies";
                        break;
                    case 3:
                        item.clientName = Context.Hospitals.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Hospitals";
                        break;
                    case 4:
                        item.clientName = Context.Distributers.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Distributers";
                        break;
                }
            }
            return list;
        }

        public List<DtoOrders> getOrdersByAgentId(int agentId, string lang)
        {
            var list = new List<DtoOrders>();
            if (lang == "en")
            {
                list = (from q in Context.Orders
                        where q.deletedBy == null && q.agentId == agentId
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentId = q.agentId,
                            subject = q.subject,
                            agentName = q.Agent1.ContactName,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,

                            deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                            supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            noOfItems = q.noOfItems,
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditName = q.Agent2.ContactName,
                            lastEditDate = q.lastEditDate
                        }).ToList();
            }
            else
            {
                list = (from q in Context.Orders
                        where q.deletedBy == null && q.agentId == agentId
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentId = q.agentId,
                            subject = q.subject,
                            agentName = q.Agent1.ContactName,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,
                            deliverdStatus = q.isDeliverd != null ? ((bool)q.isDeliverd ? "Deliverd" : "Not Deliverd") : "Pending",
                            supervisorStatus = q.supervisorApprove != null ? ((bool)q.supervisorApprove ? "Approved" : "Not Approved") : "Pending",
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            noOfItems = q.noOfItems,
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditName = q.Agent2.ContactName,
                            lastEditDate = q.lastEditDate
                        }).ToList();
            }
            foreach (DtoOrders item in list)
            {
                var obj = Context.DefaultLists.FirstOrDefault(x => x.Id == item.orderTypeId).Action;

                switch (obj)
                {
                    case 1:
                        item.clientName = Context.Docotors.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Doctors";
                        break;
                    case 2:
                        item.clientName = Context.Pharmacies.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Pharmacies";
                        break;
                    case 3:
                        item.clientName = Context.Hospitals.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Hospitals";
                        break;
                    case 4:
                        item.clientName = Context.Distributers.FirstOrDefault(x => x.Id == item.orderTo).Name;
                        item.orderTypeName = "Distributers";
                        break;
                }
            }
            return list;
        }

        public DtoOrders selectById(int id, string lang)
        {
            var list = new DtoOrders();
            if (lang == "en")
            {
                list = (from q in Context.Orders
                        where q.id == id
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentId = q.agentId,
                            subject = q.subject,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            // isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            noOfItems = q.noOfItems,
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditDate = q.lastEditDate,
                            deletedBy = q.deletedBy,
                        }).FirstOrDefault();
            }
            else
            {
                list = (from q in Context.Orders
                        where q.id == id
                        select new DtoOrders
                        {
                            id = q.id,
                            orderTo = q.orderTo,
                            orderTypeId = q.orderTypeId,
                            agentId = q.agentId,
                            subject = q.subject,
                            orderDate = q.orderDate,
                            estimateDate = q.estimateDate,
                            deliverdDate = q.deliverdDate,
                            supervisorApprove = q.supervisorApprove,
                            // isDeliverd = q.isDeliverd,
                            supervisorDate = q.supervisorDate,
                            noOfItems = q.noOfItems,
                            total = q.total,
                            netTotal = q.netTotal,
                            lastEditBy = q.lastEditBy,
                            lastEditDate = q.lastEditDate,
                            deletedBy = q.deletedBy,
                        }).FirstOrDefault();
            } return list;
        }

        public List<DtoSummaryWords> AlertsByOrders()
        {
            var list = new List<DtoSummaryWords>();

            DateTime? currentDate = DateTime.Now.Date;

            var week = (from o in Context.Orders
                        where o.deletedBy == null
                        select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-7)).Count();

            var week2 = (from o in Context.Orders
                         where o.deletedBy == null

                         select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-14) && o.orderDate <= currentDate.Value.AddDays(-7)).Count();

            var moreWeeks = (from o in Context.Orders
                             where o.deletedBy == null

                             select o).ToList().Where(o => o.orderDate <= currentDate.Value.AddDays(-14)).Count();

            list.Add(new DtoSummaryWords { item = "This Week", total = week });
            list.Add(new DtoSummaryWords { item = "Two Week", total = week2 });
            list.Add(new DtoSummaryWords { item = "More Two Week", total = moreWeeks });
            return list;
        }

        public List<DtoSummaryWords> AlertsApproved()
        {
            var list = new List<DtoSummaryWords>();

            DateTime? currentDate = DateTime.Now.Date;

            var week = (from o in Context.Orders
                        where o.supervisorApprove == true && o.deletedBy == null
                        select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-7)).Count();

            var week2 = (from o in Context.Orders
                         where o.supervisorApprove == true && o.deletedBy == null
                         select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-14) && o.orderDate <= currentDate.Value.AddDays(-7)).Count();

            var moreWeeks = (from o in Context.Orders
                             where o.supervisorApprove == true && o.deletedBy == null

                             select o).ToList().Where(o => o.orderDate <= currentDate.Value.AddDays(-14)).Count();

            list.Add(new DtoSummaryWords { item = "This Week", total = week });
            list.Add(new DtoSummaryWords { item = "Two Week", total = week2 });
            list.Add(new DtoSummaryWords { item = "More Two Week", total = moreWeeks });
            return list;
        }

        public List<DtoSummaryWords> AlertsOrdersandApprvoved()
        {
            var list = new List<DtoSummaryWords>();

            DateTime? currentDate = DateTime.Now.Date;

            var ordersApproved = (from o in Context.Orders
                                  where o.supervisorApprove == true && o.deletedBy == null
                                  select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-7)).Count();

            var ordersApplied = (from o in Context.Orders
                                 where o.deletedBy == null
                                 select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-7)).Count();

            var ordersCompleted = (from o in Context.Orders
                                   where o.isDeliverd == true && o.deletedBy == null
                                   select o).ToList().Where(o => o.orderDate >= currentDate.Value.AddDays(-7)).Count();


            list.Add(new DtoSummaryWords { item = "Orders Was Supervisor Approved", total = ordersApproved });

            list.Add(new DtoSummaryWords { item = "Orders Was Applied", total = ordersApplied });

            list.Add(new DtoSummaryWords { item = "Orders Was Completed", total = ordersApplied });
            return list;
        }
    }
}
