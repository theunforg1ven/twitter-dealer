export interface MainUserModel {
    userTwId: number;
    followersCount: number;
    userTwName: string;
    imageUrl: string;
    url: string;
    isProtected?: boolean;
    screenName: string;
    location: string;
    friendsCount: number;
    profileBackgroundColor: string;
    profileTextColor: string;
    profileLinkColor: string;
    profileBackgroundImageUrl: string;
    favouritesCount: number;
    listedCount: number;
    statusesCount: number;
    isProfileBackgroundTiled: number;
    isVerified?: number;
    isGeoEnabled?: boolean;
    language: string;
    createdDate?: Date;
}
