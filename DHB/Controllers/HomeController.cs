using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DHB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using DHB.VM.Home;

namespace DHB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        DHBContext db;

        public HomeController(DHBContext user)
        {
            db = user;
        }
        public async Task<IActionResult> Oper(VM.Home.Oper op, string st, int? id)
        {
            IQueryable<Models.Oper> operat = db.Operations.Include(p => p.Plan).Include(c => c.Ps);
            operat = operat.Where(p => p.Plan.User.Email == User.Identity.Name);
            if (id != null)
            {
                operat = operat.Where(p => p.PlanId == id);
                VM.Home.Oper op1 = new VM.Home.Oper();
                op1.id = id;
                op1.Operations = operat;
                return View(op1);
            }
            else
            {
                if (st == "выполняются")
                {
                    operat = operat.Where(p => p.Plan.Data <= DateTime.Now && p.Plan.DataPeriod >= DateTime.Now);
                    foreach (Models.Oper op1 in operat)
                    {
                        int pr = op1.Sum / 100;// 1%
                        int s = 0;
                        foreach (Models.P p in op1.Ps)
                        {
                            s += p.Sum;
                        }
                        op1.SumP = s;
                        op1.Procent = s / pr;
                        db.Operations.Update(op1);

                    }
                    await db.SaveChangesAsync();
                    VM.Home.Oper op2 = new VM.Home.Oper();
                    op2.Operations = operat;
                    return View(op2);
                }
                else if (st == "ожидают")
                {
                    operat = operat.Where(p => p.Plan.Data > DateTime.Now);
                    VM.Home.Oper op2 = new VM.Home.Oper();
                    op2.Operations = operat;
                    return View(op2);
                }
                else if (st == "выполнены")
                {
                    operat = operat.Where(p => p.Plan.DataPeriod < DateTime.Now);
                    VM.Home.Oper op2 = new VM.Home.Oper();
                    op2.Operations = operat;
                    return View(op2);
                }
                else
                {
                    VM.Home.Oper op2 = new VM.Home.Oper();
                    op2.Operations = operat;
                    return View(op2);
                }
            }


        }
        public async Task<IActionResult> Plan(VM.Home.Plan plan, int? id, string st)
        {
            IQueryable<Models.Plan> pl = db.Plans.Include(c => c.User).Include(u => u.Opers).ThenInclude(u => u.Ps);
            pl = pl.Where(p => p.User.Email == User.Identity.Name);
            if (id != null)
            {
                pl = pl.Where(p => p.Id == id);
                VM.Home.Plan pl2 = new VM.Home.Plan();
                pl2.pl = pl;
                return View(pl2);
            }
            else
            {
                if (st == "выполняются")
                {
                    pl = pl.Where(p => p.Data <= DateTime.Now && p.DataPeriod >= DateTime.Now);
                    foreach (Models.Plan p in pl)
                    {
                        int i = 0;
                        int t = 0;
                        int ras = 0;
                        int doch = 0;
                        foreach (Models.Oper op in p.Opers)
                        {
                            if (op.NameAct == "доход")
                            {
                                doch += op.SumP;
                            }
                            else
                            {
                                ras += op.SumP;
                            }
                            i += op.Procent;
                            t++;
                        }
                        p.RasMonth = ras;
                        p.DochMonth = doch;
                        p.RaznDochRas = doch - ras;
                        if (i != 0)
                        {
                            p.Procent = i / t;
                        }
                        else
                        {
                            p.Procent = 0;
                        }
                        db.Plans.Update(p);
                    }
                    await db.SaveChangesAsync();
                    VM.Home.Plan pl1 = new VM.Home.Plan();
                    pl1.pl = pl;
                    pl1.St = "выполняется";
                    return View(pl1);
                }
                else if (st == "ожидают")
                {
                    pl = pl.Where(p => p.Data > DateTime.Now);
                    VM.Home.Plan pl1 = new VM.Home.Plan();
                    pl1.pl = pl;
                    pl1.St = "ожидают";
                    return View(pl1);
                }
                else if (st == "выполнены")
                {
                    pl = pl.Where(p => p.DataPeriod < DateTime.Now);
                    VM.Home.Plan pl1 = new VM.Home.Plan();
                    pl1.pl = pl;
                    pl1.St = "выполнены";
                    return View(pl1);
                }
                else
                {
                    VM.Home.Plan pl1 = new VM.Home.Plan();
                    pl1.pl = pl;
                    pl1.St = "все";
                    return View(pl1);
                }
            }

        }
        public IActionResult P(VM.Home.P p, int? id)
        {
            IQueryable<Models.P> p1 = db.Ps.Include(c => c.Operation);
            p1 = p1.Where(p => p.Operation.Plan.User.Email == User.Identity.Name);
            if (id != null)
            {
                p1 = p1.Where(p => p.OperationId == id);
                VM.Home.P p2 = new VM.Home.P();
                p2.ps = p1;
                return View(p2);
            }
            else
            {
                VM.Home.P p2 = new VM.Home.P();
                p2.ps = p1;
                return View(p2);
            }
        }
        public IActionResult P1(VM.Home.P p)
        {
            IQueryable<Models.P1> p1 = db.P1s.Include(c => c.User);
            p1 = p1.Where(p => p.User.Email == User.Identity.Name);
            VM.Home.P1 p2 = new VM.Home.P1();
            p2.p1s = p1;
            return View(p2);
        }
    }
}
