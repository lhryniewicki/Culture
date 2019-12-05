import { Modal } from 'react-bootstrap';
import React from 'react';
import Comment from '../Comment/Comment';
import Reactions from '../Reactions/Reactions';
import { sendComment, getMoreComments } from '../../api/CommentApi';
import { sendReaction } from '../../api/EventApi';
import { userIsAuthenticated } from '../../utils/JwtUtils';
import { Link } from 'react-router-dom';
import defaultImage from '../../assets/default_avatar.jpg';
import { deleteComment } from '../../api/CommentApi';


const images = {
    like: require('../../assets/reactions/like.svg'),
    love: require('../../assets/reactions/love.svg'),
    wow: require('../../assets/reactions/wow.svg'),
    haha: require('../../assets/reactions/haha.svg'),
    angry: require('../../assets/reactions/angry.svg'),
    sad: require('../../assets/reactions/sad.svg')
};
class CommReactionBar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
            comment: '',
            showReactionModal: false,
            showComments: false,
            commentsPage: 1,
            mouseCoords: 0,
            reactionsCount: 0,
            comments: [],
            reactions: [],
            currentReaction: null,
            commentsCount: 0,
            image: null,
            imageClicked: false
        };

        this.showComments = this.showComments.bind(this);
        this.moreComments = this.moreComments.bind(this);
        this.onReactionClick = this.onReactionClick.bind(this);
        this.onReactionSend = this.onReactionSend.bind(this);
        this.closeModal = this.closeModal.bind(this);
        this.handleInputChange = this.handleInputChange.bind(this);
        this.displayComments = this.displayComments.bind(this);
    }
    componentDidMount() {
        this.setState({
            reactionsCount: this.props.reactionsCount,
            reactions: this.props.reactions,
            comments: this.props.comments,
            commentsCount: this.props.commentsCount,
            currentReaction: this.props.currentReaction,
            canLoadMoreComments: this.props.canLoadMore
        });

    }
    componentDidUpdate(prevProps) {

        if (prevProps.reactionsCount !== this.props.reactionsCount || this.props.commentsCount !== prevProps.commentsCount) {

            this.setState({
                reactionsCount: this.props.reactionsCount,
                reactions: this.props.reactions,
                comments: this.props.comments,
                commentsCount: this.props.commentsCount,
                currentReaction: this.props.currentReaction,
                canLoadMoreComments: this.props.canLoadMore
            });
        }
    }


    handleCommentChange = (evt, text) => {

        const item = document.getElementById('comm');

        this.setState({ [evt.target.name]: evt.target.value });
        item.style.height = (25 + text.scrollHeight) + "px";

    }

    handleInputChange(e) {
        
        this.setState({ [e.target.name]: e.target.value });
    }
    closeModal() {
        this.setState({ showReactionModal: false });
    }
    onReactionClick(e) {
        e.preventDefault();
        if (this.props.isPreview === true) return false;
        this.setState({
            mouseCoords: e.clientY
            ,
            showReactionModal: true
        });
    }
    async onReactionSend(e) {
        const name = e.target.name;
        const result = await sendReaction(this.props.id, name);

        if (result === undefined) return false;
        if (name === this.state.currentReaction) {

            this.setState({
                currentReaction: null,
                reactions: result.reactions,
                reactionsCount: this.state.reactionsCount - 1
            });
            console.log(this.state.currentReaction);
        }
        else if (this.state.currentReaction === null) {

            this.setState({
                currentReaction: name,
                reactions: result.reactions,
                reactionsCount: this.state.reactionsCount + 1
            });
        }
        else {

            this.setState({
                currentReaction: name,
                reactions: result.reactions
            });
        }
        this.closeModal();
        console.log(this.state.reactions);
    }
    showComments(event) {
        event.preventDefault();
        if (this.props.isPreview === true) return false;
        this.setState({
            showComments: !this.state.showComments
        });

    }

    deleteComment = (e, commentId) => {
        e.preventDefault();

        deleteComment(commentId);

        this.setState({
            commentsCount: this.state.commentsCount -1,
            comments: this.state.comments.filter(x => {return x.id !== commentId })
        });

    }

    displayComments() {
        console.log(this.state.comments)
        const items = this.state.comments.map((c, index) => {

            let jsDate = new Date(Date.parse(c.creationDate));
            console.log(jsDate.getDay())
            console.log(jsDate.getDate())

            let jsDateFormatted = `${jsDate.getDate()}-${jsDate.getMonth()+1}-${jsDate.getFullYear()} ${jsDate.getHours()}:${(jsDate.getMinutes() < 10 ? '0' : '') + jsDate.getMinutes()}`;

            return (
                <Comment
                    key={index}
                    content={c.content}
                    author={c.authorName}
                    creationDate={jsDateFormatted}
                    image={c.imagePath}
                    authorId={c.authorId}
                    avatarPath={c.avatarPath}
                    commentId={c.id}
                    deleteComment={this.deleteComment}
                />
            );
        });

        return items;
    }
    async moreComments(event) {
        event.preventDefault();
        const newComments = await getMoreComments(this.props.id, this.state.commentsPage);
        const items = this.state.comments.concat(newComments.commentsList);
        this.setState({
            commentsPage: this.state.commentsPage + 1,
            comments: items,
            canLoadMoreComments: newComments.canLoadMore
        });
    }
    displaySortedReactions() {
        if (this.state.reactions === null || this.state.reactions === undefined || this.state.reactions.length < 1) {
            return [];
        }
        return this.state.reactions.map((element, index) => {
            return (<img draggable={false}
                name={element.reactionType}
                src={images[element.reactionType]}
                width="25px"
                height="25px"
                data-toggle="popover"
                key={index}
                data-placement="top"
                title={`Ilość reakcji: ` + element.count} />);
        });
    }

    handleFilePick = (event) => {
        if (event.target.files[0] !== undefined) {
            this.setState({
                image: event.target.files[0]
            });
        }
    }

    displayImageZoom = () => {
        return <Modal style={{ left: "-10%" }} show={this.state.imageClicked} onHide={this.closeImageModal} >
            <img
                width="700px"
                height="700px"
                src={URL.createObjectURL(this.state.image)} />
        </Modal>
    }

    handleImageClick = () => {
        this.setState({ imageClicked: true });

    }

    handleCommentSubmit = async (e) => {
        e.preventDefault();

        if (this.props.isPreview === true) return false;
        console.log(this.state.co)
        const newComment = await sendComment(this.props.id, this.state.content, this.state.image);
        console.log(newComment)
        this.setState(prevState => ({
            showComments: true,
            imageClicked: false,
            image: null,
            content: '',
            //commentsCount: this.state.commentsCount + 1
            //comments: [newComment, ...prevState.comments]
        }));
    }

    closeImageModal = () => {
        this.setState({ imageClicked: false });
    }

    displayImageCancelButton = () => {
        this.setState({ image: null });
        this.fileInput.value = '';

    }

    displayCurrentReaction = () => {
        if (this.state.currentReaction !== null && this.state.currentReaction !== undefined)
            return <img
                src={images[this.state.currentReaction]}
                data-placement="top"
                title={`Twoja reakcja`}
                data-toggle="popover"
                width="25px"
                height="25px"
            />;
    }
    render() {
        return (
            <div>
                <div className="card-footer text-muted" >
                    Umieszczono dnia {this.props.date} przez
            <Link to={`/konto/${this.props.createdById}`}> {this.props.createdBy} </Link>
                    {
                        this.displaySortedReactions()
                    }

                    {this.state.reactionsCount}
                    <div className="pull-right">
                        <div className="float-left">
                            {userIsAuthenticated() ?
                                <a href="" onClick={this.onReactionClick} className="comment" style={{ marginRight: "50px" }}> Reaguj {this.displayCurrentReaction()}</a>
                                :
                                null
                            }
                            <a href="" onClick={this.showComments} className="comment">  Komentarze: {this.state.commentsCount}</a>
                        </div>
                    </div>

                    <Reactions images={images}
                        onReactionSend={this.onReactionSend}
                        mouseCoords={this.state.mouseCoords}
                        closeModal={this.closeModal}
                        showModal={this.state.showReactionModal} />

                </div>
                {this.state.showComments === true
                    ?
                    <div>
                        {
                            this.displayComments()
                        }
                        {this.state.canLoadMoreComments === true
                            ?
                            <div className="text-center">
                                <a href="#" onClick={this.moreComments}>Załaduj więcej</a>
                            </div>
                            :
                            null
                        }
                    </div>

                    :
                    null
                }
                 {
                  userIsAuthenticated() ?
                    <div>
                        <div className="card-footer text-muted " >
                            <form onSubmit={this.handleCommentSubmit}>
                                
                                <div className="input-container">
                                    <img
                                        src={this.props.avatarPath !== null ? this.props.avatarPath : defaultImage}
                                        width="50px"
                                        height="50px"
                                        className="avatarPreview"
                                    />
                                        <input className="form-control  commentBox"
                                            placeholder="Wpisz komentarz..."
                                            onChange={(e) => this.handleCommentChange(e, this)}
                                            required
                                        type="text"
                                        value={this.state.content}
                                        name="content"
                                        id="comm"
                                        style={{ overflow:"hidden" }}
                                        autoComplete="off"
                                        width="400px" />
                                    <input
                                        style={{ display: "none" }}
                                        accept=".jpg, .jpeg, .png"
                                        type="file"
                                        onChange={this.handleFilePick}
                                        ref={fileInput => this.fileInput = fileInput}
                                    />
                                        <i className="far fa-images icon"
                                            onClick={this.props.isPreview ? null : () => this.fileInput.click()}
                                    />
                                </div>
                            </form>
                            {this.state.image === null ?
                                null :
                                <div>
                                    <img
                                        onClick={this.handleImageClick}
                                        width="300px"
                                        height="300px"
                                        src={URL.createObjectURL(this.state.image)} />
                                    {this.state.image !== null ? <button onClick={this.displayImageCancelButton} className="btn btn-danger align-top">X</button> : null}

                                </div>
                            }
                        </div>
                    
                        {this.state.imageClicked === true ? this.displayImageZoom() : null}
                    </div>
                :
                null
                }
            </div>

        );
    }
}
export default CommReactionBar;



