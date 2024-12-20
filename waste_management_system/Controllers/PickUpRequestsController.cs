using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using waste_management_system.Data;
using waste_management_system.Models;

namespace waste_management_system.Controllers
{
    [Authorize]
    public class PickUpRequestsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PickUpRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public void getClient(int reqID)
        {
            var company = from req in _context.PickUpRequests
                          join user in _context.UserProfiles
                          on req.UserProfileId equals user.UserProfileId
                          where req.PickUpRequestId == reqID
                          select new
                          {
                              company = user.CompanyName
                          };

            var companyName = company.FirstOrDefault();

            ViewBag.companyName = companyName.company;
        }

        public void ViewDataElements()
        {
            ViewBag.Waste = new SelectList(_context.TypeOfWastes, "TypeOfWasteId", "Name");
            ViewBag.Address = new SelectList(_context.UserProfiles, "Address", "Address");

            ViewData["TypeOfWasteId"] = new SelectList(_context.TypeOfWastes, "TypeOfWasteId", "Name");
            ViewData["UserProfileId"] = new SelectList(_context.UserProfiles, "UserProfileId", "Responsible");
            ViewData["AddresResponsible"] = new SelectList(_context.UserProfiles, "UserProfileId", "Address");
        }

        [HttpGet]
        public JsonResult getVehicleByWaste(int id)
        {
            try
            {
                var typeVehicle = from vehicle in _context.Vehicles
                                  join waste in _context.TypeOfWastes
                                  on vehicle.VehicleId equals waste.VehicleId
                                  where waste.VehicleId == id
                                  select new
                                  {
                                      typeVehicle = vehicle.VehicleClass,
                                  };

                var element = typeVehicle.FirstOrDefault();

                if (element == null)
                {
                    return Json(new { success = false, description = string.Empty });
                }
                else
                {
                    return Json(new { success = true, description = element.typeVehicle });
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // GET: PickUpRequests
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            try
            {
                ViewDataElements();

                var GetAllRequest = from pickup in _context.PickUpRequests
                                    join user in _context.UserProfiles
                                    on pickup.UserProfileId equals user.UserProfileId
                                    join waste in _context.TypeOfWastes
                                    on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                    join status in _context.RequestStatuses
                                    on pickup.RequestStatusId equals status.RequestStatusId
                                    select new
                                    {
                                        pickupID = pickup.PickUpRequestId,
                                        addressRequest = user.Address,
                                        typeWaste = waste.Name,
                                        pickupDate = pickup.PickupDate,
                                        pickupRequest = pickup.RequestDate,
                                        statusRequest = status.Status
                                    };

                var listRequest = GetAllRequest.ToList();

                var userProfile = GetCurrentUserProfile();
                ViewBag.CompanyName = userProfile?.CompanyName;

                return View(listRequest);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }

        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(string address, DateTime dateRequest, DateTime datePickup, int typeWasteID)
        {
            try
            {
                ViewDataElements();


                if (address != null && dateRequest.ToString() != "01-01-0001" && datePickup.ToString() != "01-01-0001" && typeWasteID != 0)
                {
                    var GetAllRequest = from pickup in _context.PickUpRequests
                                        join user in _context.UserProfiles
                                        on pickup.UserProfileId equals user.UserProfileId
                                        join waste in _context.TypeOfWastes
                                        on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                        join status in _context.RequestStatuses
                                        on pickup.RequestStatusId equals status.RequestStatusId
                                        where pickup.PickupDate == DateTime.Parse(datePickup.ToString("yyyy-MM-dd")) && pickup.RequestDate == DateTime.Parse(dateRequest.ToString("yyyy-MM-dd")) && pickup.TypeOfWasteId == typeWasteID && user.Address == address
                                        select new
                                        {
                                            pickupID = pickup.PickUpRequestId,
                                            addressRequest = user.Address,
                                            typeWaste = waste.Name,
                                            pickupDate = pickup.PickupDate,
                                            pickupRequest = pickup.RequestDate,
                                            statusRequest = status.Status
                                        };

                    var listRequest = GetAllRequest.ToList();

                    return View(listRequest);
                }
                else if (address != null || dateRequest.ToString("dd-MM-yyyy") != "01-01-0001" || datePickup.ToString("dd-MM-yyyy") != "01-01-0001" || typeWasteID != 0)
                {
                    var GetAllRequest = from pickup in _context.PickUpRequests
                                        join user in _context.UserProfiles
                                        on pickup.UserProfileId equals user.UserProfileId
                                        join waste in _context.TypeOfWastes
                                        on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                        join status in _context.RequestStatuses
                                        on pickup.RequestStatusId equals status.RequestStatusId
                                        where pickup.PickupDate == DateTime.Parse(datePickup.ToString("yyyy-MM-dd")) || pickup.RequestDate == DateTime.Parse(dateRequest.ToString("yyyy-MM-dd")) || pickup.TypeOfWasteId == typeWasteID || user.Address == address
                                        select new
                                        {
                                            pickupID = pickup.PickUpRequestId,
                                            addressRequest = user.Address,
                                            typeWaste = waste.Name,
                                            pickupDate = pickup.PickupDate,
                                            pickupRequest = pickup.RequestDate,
                                            statusRequest = status.Status
                                        };

                    var listRequest = GetAllRequest.ToList();

                    return View(listRequest);
                }
                else
                {
                    var GetAllRequest = from pickup in _context.PickUpRequests
                                        join user in _context.UserProfiles
                                        on pickup.UserProfileId equals user.UserProfileId
                                        join waste in _context.TypeOfWastes
                                        on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                        join status in _context.RequestStatuses
                                        on pickup.RequestStatusId equals status.RequestStatusId
                                        select new
                                        {
                                            pickupID = pickup.PickUpRequestId,
                                            addressRequest = user.Address,
                                            typeWaste = waste.Name,
                                            pickupDate = pickup.PickupDate,
                                            pickupRequest = pickup.RequestDate,
                                            statusRequest = status.Status
                                        };

                    var listRequest = GetAllRequest.ToList();

                    return View(listRequest);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }
        }

        // GET: PickUpRequests/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                ViewBag.RequestNumber = id;
                getClient((int)id);
                 
                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var getRequestObject = from pickup in _context.PickUpRequests
                                           join observation in _context.Observations
                                           on pickup.PickUpRequestId equals observation.PickUpRequestId
                                           join user in _context.UserProfiles
                                           on pickup.UserProfileId equals user.UserProfileId
                                           join waste in _context.TypeOfWastes
                                           on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                           join vehicle in _context.Vehicles
                                           on waste.VehicleId equals vehicle.VehicleId
                                           where pickup.PickUpRequestId == id
                                           select new
                                           {
                                               companyName = user.CompanyName,
                                               responsible = user.Responsible,
                                               email = user.Email,
                                               requestDate = pickup.RequestDate,
                                               pickupDate = pickup.PickupDate,
                                               wasteName = waste.Name,
                                               typeVehicle = vehicle.VehicleClass

                                           };

                    var elementList = from observationElement in _context.Observations
                                      where observationElement.PickUpRequestId == id
                                      select new
                                      {
                                          observationUser = observationElement.Description,
                                          observationDate = observationElement.EntryDate,
                                          observationAuthor = observationElement.AuthorName
                                      };

                    var requestObject = getRequestObject.FirstOrDefault();

                    if (requestObject == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.Observation = elementList.OrderByDescending(x => x.observationDate).ToList();
                        return View(requestObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }
        }

        // GET: PickUpRequests/Create
        public IActionResult Create()
        {
            ViewDataElements();
            var userProfile = GetCurrentUserProfile();
            ViewBag.Address = userProfile?.Address;
            ViewBag.CompanyName = userProfile?.CompanyName;
            ViewBag.Responsible = userProfile?.Responsible;
            return View();
        }

        // POST: PickUpRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int TypeOfWasteId, DateTime pickUpDate, string observationREQ, string Author) 
        {
            try
            {
                PickUpRequest pickUpReq = new PickUpRequest();

                pickUpReq.RequestStatusId = 1;
                pickUpReq.UserProfileId = 1;
                pickUpReq.RequestDate = DateTime.Now;
                pickUpReq.TypeOfWasteId = TypeOfWasteId;
                pickUpReq.PickupDate = pickUpDate;

                _context.Add(pickUpReq);
                await _context.SaveChangesAsync();

                var newID = pickUpReq.PickUpRequestId;

                Author = "John Smith";

                Observation observation = new Observation();
                observation.Description = observationREQ;
                observation.AuthorName = Author;
                observation.PickUpRequestId = newID;
                observation.EntryDate = DateTime.Now;
                _context.Add(observation);
                await _context.SaveChangesAsync();

                ViewBag.msj = "Your pickup request was successfully created on " + DateTime.Now.ToString("dd-MM-yyyyy") + " with the associated number " + newID + ".";

                return View("Create");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: { ex.Message }");
                return NotFound();
            }
        }

        // GET: PickUpRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                ViewBag.RequestNumber = id;
                ViewDataElements();
                getClient((int)id);

                if (id == null)
                {
                    return NotFound();
                }
                else
                {
                    var getRequestObject = from pickup in _context.PickUpRequests
                                           join observation in _context.Observations
                                           on pickup.PickUpRequestId equals observation.PickUpRequestId
                                           join user in _context.UserProfiles
                                           on pickup.UserProfileId equals user.UserProfileId
                                           join waste in _context.TypeOfWastes
                                           on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                           join vehicle in _context.Vehicles
                                           on waste.VehicleId equals vehicle.VehicleId
                                           where pickup.PickUpRequestId == id
                                           select new
                                           {
                                               companyName = user.CompanyName,
                                               responsible = user.Responsible,
                                               email = user.Email,
                                               requestDate = pickup.RequestDate,
                                               pickupDate = pickup.PickupDate,
                                               wasteName = waste.Name,
                                               typeVehicle = vehicle.VehicleClass

                                           };

                    var elementList = from observationElement in _context.Observations
                                      where observationElement.PickUpRequestId == id
                                      select new
                                      {
                                          observationUser = observationElement.Description,
                                          observationDate = observationElement.EntryDate,
                                          observationAuthor = observationElement.AuthorName
                                      };

                    var requestObject = getRequestObject.FirstOrDefault();

                    if (requestObject == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        ViewBag.Observation = elementList.OrderByDescending(x => x.observationDate).ToList();
                        return View(requestObject);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }
        }

        // POST: PickUpRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DateTime pickupDate, int typeWasteID, string observation, string author)
        {
            var req = await _context.PickUpRequests.FindAsync(id);

            try
            {
                if (req == null)
                {
                    return NotFound();
                }
                else
                {
                    req.PickupDate = pickupDate;
                    req.TypeOfWasteId = typeWasteID;

                    author = "John Smith";

                    Observation observationUpdate = new Observation();
                    observationUpdate.Description = observation;
                    observationUpdate.AuthorName = author;
                    observationUpdate.EntryDate = DateTime.Now;
                    observationUpdate.PickUpRequestId = id;
                    _context.Add(observationUpdate);
                }
                
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }
        }

        // GET: PickUpRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                getClient((int)id);
                ViewBag.RequestNumber = id;

                var pickupREQ = from pickup in _context.PickUpRequests
                                       join observation in _context.Observations
                                       on pickup.PickUpRequestId equals observation.PickUpRequestId
                                       join user in _context.UserProfiles
                                       on pickup.UserProfileId equals user.UserProfileId
                                       join waste in _context.TypeOfWastes
                                       on pickup.TypeOfWasteId equals waste.TypeOfWasteId
                                       join vehicle in _context.Vehicles
                                       on waste.VehicleId equals vehicle.VehicleId
                                       join status in _context.RequestStatuses
                                       on pickup.RequestStatusId equals status.RequestStatusId
                                       where pickup.PickUpRequestId == id
                                       select new
                                       {
                                           companyName = user.CompanyName,
                                           responsible = user.Responsible,
                                           email = user.Email,
                                           requestDate = pickup.RequestDate,
                                           pickupDate = pickup.PickupDate,
                                           wasteName = waste.Name,
                                           typeVehicle = vehicle.VehicleClass,
                                           statusName = status.Status,
                                           observationUser = observation.Description,
                                           observationDate = observation.EntryDate
                                       };

                var requestObject = pickupREQ.FirstOrDefault();

                return View(requestObject);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return NotFound();
            }
        }

        // POST: PickUpRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pickUpRequest = await _context.PickUpRequests.FindAsync(id);
            if (pickUpRequest != null)
            {
                _context.PickUpRequests.Remove(pickUpRequest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PickUpRequestExists(int id)
        {
            return _context.PickUpRequests.Any(e => e.PickUpRequestId == id);
        }

        private UserProfile GetCurrentUserProfile()
        {
            var userEmail = User.Identity.Name;

            var userProfile = _context.UserProfiles.FirstOrDefault(u => u.Email == userEmail);

            return userProfile;
        }
    }
}
