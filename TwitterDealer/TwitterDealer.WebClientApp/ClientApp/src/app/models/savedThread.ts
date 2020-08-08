export interface SavedThread {
    id: number;
    isFavourite: boolean;
    retweetCount: number;
    tweetText: string;
    language: string;
    isPossiblySensitive?: boolean;
    url: string;
    favoriteCount: number;
    created?: Date;
}