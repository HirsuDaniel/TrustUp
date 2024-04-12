import { Component } from '@angular/core';
import { AuthService } from 'app/services/auth/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss'
})
export class RegisterComponent{

  name: string = '';
  email: string = '';
  password: string = '';
  address: string = '';


  constructor(private authService: AuthService, private router: Router){}


  onSubmit(): void {
    this.authService.register(this.name, this.email, this.password, this.address).subscribe(
      () => {
        console.log('Registration successful');
        this.router.navigate(['/login']); 
      },
      error => {
        console.log('Registration failed:', error);
        // Handle registration error (e.g., display error message)
      }
    );
  }
}
