import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { StatusTweet } from '../models/statusTweet';
import { UserMedia } from '../models/userMedia';
import { MainUserModel } from '../models/mainUserModel';

@Injectable({
  providedIn: 'root'
})
export class TweetsService {
  readonly rootUrl = environment.apiUrl;
  allThread: StatusTweet[];

  constructor(private http: HttpClient) { }

  getFullThread(link: string): Observable<StatusTweet> {
    return this.http.get<StatusTweet>(this.rootUrl + 'twitterdata/twitterdata?tweetUrl=' + link);
  }

  getUserThread(link: string): Observable<StatusTweet> {
    return this.http.get<StatusTweet>(this.rootUrl + 'twitterthread/gettwitterthread?tweetUrl=' + link);
  }

  getUserTweets(link: string): Observable<StatusTweet[]> {
    return this.http.get<StatusTweet[]>(this.rootUrl + 'twitteruser/twitterusertweets?screenName=' + link);
  }

  getUserMedia(link: string): Observable<UserMedia[]> {
    return this.http.get<UserMedia[]>(this.rootUrl + 'twitteruser/twitterusermedia?screenName=' + link);
  }

  getUserInfo(link: string): Observable<MainUserModel> {
    return this.http.get<MainUserModel>(this.rootUrl + 'twitteruser/twitteruserprofile?screenName=' + link);
  }
}
