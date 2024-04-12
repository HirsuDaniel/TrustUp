import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private isAuthenticated = false;
  private tokenKey = 'token';


  private apiUrl = 'http://localhost:5084/api/users'; 

  constructor(private http: HttpClient) {}

  login(email: string, password: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/login`, { email, password })
      .pipe(
        tap(response => {
          if (response && response.token) {
            localStorage.setItem(this.tokenKey, response.token);
            this.isAuthenticated = true;
          }
        })
      );
  }

  register(name: string, email: string, password: string, address: string): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/register`, { name, email, password, address });
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
    this.isAuthenticated = false;
  }

  isLoggedIn(): boolean {
    return this.isAuthenticated;
  }
}
