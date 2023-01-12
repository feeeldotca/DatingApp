import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EMPTY, ReplaySubject } from 'rxjs';
import { map } from 'rxjs/operators';
import { User } from '../models/user';
import { Users } from '../models/user.modle';


@Injectable({
  providedIn: 'root'
})

export class AccountService {
  baseUrl = 'https://localhost:5001/api/';
  private currentUserSource = new ReplaySubject<User>(1);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http:HttpClient) { }
  login(model: User) {
    return this.http.post(this.baseUrl + 'account/login', model).pipe(map(
      (response: any) => {
        const user = response;
        if(user){
          this.setCurrentUser(user);
        }
      }
    ));
  }

  register(model: Users){
    return this.http.post(this.baseUrl+'account/register', model).pipe(
     map((user:any)=>{ 
        if(user){          
          this.setCurrentUser(user);
        }
     })
    )
  }

  setCurrentUser(user: User){
    localStorage.setItem('user', JSON.stringify(user));
    this.currentUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUserSource.next(undefined);
  }
}
