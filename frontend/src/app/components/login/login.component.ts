import { Component } from '@angular/core';
import { AuthService } from 'app/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss'
})
export class LoginComponent {

  email: string = '';
  password: string = '';

  constructor(private authService: AuthService, private router: Router){}


  onSubmit(): void {
    this.authService.login(this.email, this.password).subscribe(
      () => {
        console.log('Login successful');
        this.router.navigate(['/posts']); 
      },
      error => {
        console.log('Login failed:', error);
        // Handle login error (e.g., display error message)
      }
    );
  }
  
}
