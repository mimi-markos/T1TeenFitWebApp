using T1TeenFit.Core.IServices;
using T1TeenFit.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace T1TeenFit.Web.Areas.TeenUser.Controllers
{
    [Area("TeenUser")]
    public class JournalController : BaseController
    {
        private readonly IJournalService _journalService;
        private readonly IPersonaService _personaService;

        // Configuration via dependency injection
        public JournalController(IJournalService svc, IPersonaService personaService, UserManager<ApplicationUser> userManager) : base(userManager)
        {
            _journalService = svc;
            _personaService = personaService;
        }


        // GET: journal
        public IActionResult Index(bool addedJournal = false, int journalId = 0)
        {
            // if journal added, display mood feedback personas
            if(addedJournal)
            {
                var journalEntry = _journalService.GetJournalById(journalId);
                var journalFeedbackPersona = _personaService.JournalMoodFeedback(journalEntry);

                if (journalFeedbackPersona != null)
                {
                    ViewBag.JournalPersonaImageUrl = journalFeedbackPersona.ImageUrl;
                    ViewBag.JournalPersonaMessage = journalFeedbackPersona.Message;
                }
            }


            // retrieve all journals associated with user id
            var userId = GetCurrentUserId();
            var journals = _journalService.GetAllJournalsByUserId(userId);
            return View(journals);
        }


        // GET: journal/create 
        public IActionResult Create()
        {
            // display form to add journal
            var journal = new Journal
            {
                DateCreated = DateTime.Now
            };
            return View(journal);
        }


        // POST: journal/create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title, JournalMessage, DateCreated, Mood")] Journal entry)
        {
            // add journal
            if (ModelState.IsValid)
            {
                entry.ApplicationUserId = GetCurrentUserId();
                var savedJournalEntry = _journalService.AddJournal(entry);

                if (savedJournalEntry != null)
                {
                    var userId = GetCurrentUserId();
                    _personaService.JournalMoodFeedback(savedJournalEntry);
                }
                
                // if journal added, display alert notification
                Alert("Journal entry added successfully!", AlertType.success);
                return RedirectToAction(nameof(Index), new { id = entry.Id, addedJournal = true, journalId = savedJournalEntry.Id });
            }
            return View(entry);
        }


        // GET: journal/edit/{id}
        public IActionResult Edit(int id)
        {
            // verify journal exists using id
            var entry = _journalService.GetJournalById(id);

            if (entry == null)
            {
                // if journal doesn't exist, display alert notification
                Alert("Sorry, that journal entry doesn't exist.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display form to edit journal 
            return View(entry);
        }


        // POST: journal/edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([Bind("Title, JournalMessage, DateCreated, Mood, Id")] Journal entry)
        {
            // update journal 
            if (ModelState.IsValid)
            {
                entry.ApplicationUserId = GetCurrentUserId();
                _journalService.UpdateJournal(entry);

                // if journal updated, display alert notification 
                Alert("Journal entry updated successfully!", AlertType.success);
                return RedirectToAction(nameof(Index));
            }
            // if validation errors, redisplay form
            return View(entry);
        }


        // GET: journal/delete/{id}
        public IActionResult Delete(int id)
        {
            // retrieve journal by id
            var entry = _journalService.GetJournalById(id);

            // if journal doesn't exist, display alert notification 
            if (entry == null)
            {
                Alert("Sorry, that journal entry doesn't exist.", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }

            // display journal for deletion 
            return View(entry);
        }


        // POST: journal/delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmation(int id)
        {
            // delete journal and display alert notification 
            _journalService.DeleteJournal(id);

            Alert($"Journal entry deleted successfully!", AlertType.success);

            return RedirectToAction(nameof(Index));
        }
    }
}