import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatusTweet } from '../models/statusTweet';

@Injectable({
  providedIn: 'root'
})
export class TweetsService {
  readonly rootUrl = environment.apiUrl;
  allThread: StatusTweet[];

  constructor(private http: HttpClient) { }

  getFullThread(link: string): Observable<StatusTweet> {
    return this.http.get<StatusTweet>(this.rootUrl + 'twitterdata/TwitterData?tweetUrl=' + link);
  }
}
