using INKIPER.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace INKIPER.Pages;

public class Login : PageModel
{
    
    private LoginDto _loginDto { get; set; }
    
    public void OnGet()
    {
      
    }

    public async Task<IActionResult> OnPost()
    {
        return Page();
    }
}