import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = 'https://localhost:7018'; 
  private readonly http = inject(HttpClient);

  login(email:string, password:string) : Observable<string>{
        const body = { email, password };
    return this.http.post(`${this.apiUrl}/Login`, body, { responseType: 'text' });
  }

  register(name: string, lastName: string, email: string, password: string) : Observable<any>{
    const body = { name, lastName, email, password, roles: ['User'] };
    return this.http.post(`${this.apiUrl}/Register`, body);
  }


}
