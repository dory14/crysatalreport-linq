using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace crReport.Areas.User.Models
{
    public class UserModel
    {
        private static cr_reportDataContext db = new cr_reportDataContext();
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string full_name { get; set; }
        public string email { get; set; }
        public int role { get; set; }
        public string created_at { get; set; }
        public string created_by { get; set; }
        public string updated_by { get; set; }
        public string updated_at { get; set; }
        public List<UserModel> lsITDetails = new List<UserModel>();

        private static user_approval mapObject(UserModel oObj)
        {
            user_approval retVal = new user_approval();
            retVal.id = oObj.id;
            retVal.username = oObj.username;
            retVal.password = oObj.password;
            retVal.full_name = oObj.full_name;
            retVal.email = oObj.email ;
            retVal.role = oObj.role;
            return retVal;
        }
        //add data
        public void add()
        {
            using (cr_reportDataContext db = new cr_reportDataContext())
            {
                try
                {
                    db.Connection.Open();
                    db.Transaction = db.Connection.BeginTransaction();
                    user_approval soh = mapObject(this);
                    soh.created_at = DateTime.Now;
                    soh.created_by = "Dory";
                    db.user_approvals.InsertOnSubmit(soh);
                    db.SubmitChanges();

                    db.Transaction.Commit();
                }
                catch (Exception e)
                {
                    db.Transaction.Rollback();
                    throw new Exception(e.Message);
                }
                

                   
            }
            
        }

        public void getByid(int id)
        {
            using (cr_reportDataContext db = new cr_reportDataContext())
            {
               
                var data = db.user_approvals.FirstOrDefault(x => x.id == id);
                if (data == null) throw new Exception("Invalid Inventory Transfer Request");
                UserModel dat = new UserModel();
                dat.id = data.id;
                dat.username = data.username;
                dat.password = data.password;
                dat.full_name = data.full_name;
                dat.role = Convert.ToInt32(data.role);
                dat.email = data.email;
                dat.created_at = Convert.ToString(data.created_at);
                dat.created_by = data.created_by;
                dat.updated_at = Convert.ToString(data.updated_at);
                dat.updated_by = data.updated_by;
                lsITDetails.Add(dat);

            }
        }
        public static List<UserModel> getList(List<string[]> param = null)
        {
            
            
            using (cr_reportDataContext db = new cr_reportDataContext())
            {
                var query = from wtr in db.user_approvals
                            select new UserModel
                            {
                                id = wtr.id,
                                username = wtr.username,
                                password = wtr.password,
                                email = wtr.email,
                                full_name = wtr.full_name,
                                role       = Convert.ToInt32(wtr.role),
                                created_at = Convert.ToString(wtr.created_at),
                                created_by = wtr.created_by,
                                updated_at = Convert.ToString(wtr.updated_at),
                                updated_by = wtr.updated_by
            };

               
                return query.ToList();
            }
        }

        public void update()
        {
            using (cr_reportDataContext db = new cr_reportDataContext())
            {
                var data = db.user_approvals.FirstOrDefault(x => x.id == id);
                user_approval soh = mapObject(this);
                data.password = soh.password;
                data.username = soh.username;
                data.full_name = soh.full_name;
                data.role = soh.role;
                data.email = soh.email;
                data.updated_by = "dory";
                data.updated_at = DateTime.Now;
                db.SubmitChanges();
            }
        }

        public void delete(int id)
        {
            using (cr_reportDataContext db = new cr_reportDataContext())
            {
                var data = db.user_approvals.FirstOrDefault(x => x.id == id);
                db.user_approvals.DeleteOnSubmit(data);
                db.SubmitChanges();
            }
        }
    }
}