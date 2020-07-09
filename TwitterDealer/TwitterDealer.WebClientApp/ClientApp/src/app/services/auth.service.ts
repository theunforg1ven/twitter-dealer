import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly rootUrl = environment.apiUrl + 'auth';

  constructor(private http: HttpClient) { }

  login(model: any): Observable<void> {
    return this.http.post(this.rootUrl + '/login', model)
      .pipe(
        map((response: any) => {
          const user = response;
          if (user) {
            localStorage.setItem('token', user.token);
          }
        })
      );
  }

  // register(user: User) {
  //   return this.http.post(this.rootUrl + '/register', user);
  // }

  // loggedIn(): boolean {
  //   const token = localStorage.getItem('token');
  //   return !this.jwtHelper.isTokenExpired(token);
  // }
}
