import { BaseUserMedia } from './baseUserMedia';

export interface StatusTweet {
    IsFavourite: boolean;
    RetweetCount: number;
    TweetText: string;
    Language: string;
    MediaUrl: BaseUserMedia[];
    IsPossiblySensitive?: boolean;
    Url: string;
    FavoriteCount: number;
    Created?: Date;
    Username: string;
    UserScreenName: string;
    Replies: StatusTweet[];
}
