using Application.DTO.ProfileModule;
using Application.Manager.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Application.Web.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactManager _contactManager;

        public ContactController()
        {

        }

        public ContactController(IContactManager contactManager)
        {
            _contactManager = contactManager;
        }

        /// <summary>
        /// Get all profile information
        /// </summary>
        /// <returns></returns>
        public JsonResult GetAllProfiles()
        {
            IQueryable<ProfileDTO> profiles = _contactManager.FindProfiles(0, 20).AsQueryable();

            return Json(profiles, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Get profile by profile id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public JsonResult GetProfileById(int id)
        {
            var profile = _contactManager.FindProfileById(id);

            return Json(profile, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Create a new profile
        /// </summary>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPost]
        public HttpStatusCodeResult SaveProfileInformation(ProfileDTO profileDTO)
        {
            _contactManager.SaveProfileInformation(profileDTO);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Update an existing profile
        /// </summary>
        /// <param name="id"></param>
        /// <param name="profileDTO"></param>
        /// <returns></returns>
        [System.Web.Http.HttpPut]
        public HttpStatusCodeResult UpdateProfileInformation(int id, ProfileDTO profileDTO)
        {
            _contactManager.UpdateProfileInformation(id, profileDTO);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete an existing profile
        /// </summary>
        /// <param name="id"></param>
        [System.Web.Http.HttpDelete]
        public void DeleteProfile(int id)
        {
            try
            {
                if (id != 0)
                {
                    _contactManager.DeleteProfile(id);
                }
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }
        }

        /// <summary>
        /// Get all initialization data for contact page
        /// </summary>
        /// <returns></returns>
        public JsonResult InitializePageData()
        {
            var contactForm = _contactManager.InitializePageData();

            return Json(contactForm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateEdit()
        {
            return View();
        }
    }
}
