import React from 'react';


class CategoriesWidget extends React.Component {

    render() {
        return (
            <div className="card my-4">
                <h5 className="card-header">Kategorie</h5>
                <div className="card-body">
                    <div className="row">
                        <div className="col-lg-5">
                            <ul className="list-unstyled mb-0">
                                <li>
                                    <a href="#">MUZYKA</a>
                                </li>
                                <li>
                                    <a href="#">TURYSTYKA</a>
                                </li>
                                <li>
                                    <a href="#">STYL ŻYCIA</a>
                                </li>
                                <li>
                                    <a href="#">REGIONALIA</a>
                                </li>
                            </ul>
                        </div>
                        <div className="col-lg-7">
                            <ul className="list-unstyled mb-1">
                                <li>
                                    <a href="#">NAUKA I EDUKACJA</a>
                                </li>
                                <li>
                                    <a href="#">SPORT I REKREACJA</a>
                                </li>
                                <li>
                                    <a href="#">DOM I RODZINA</a>
                                </li>
                                <li>
                                    <a href="#">KULTURA I SZTUKA</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
}
export default CategoriesWidget;



