using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backend.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "discount",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    discount_percent = table.Column<int>(type: "int", nullable: false),
                    active = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_discount", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_adress",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    city = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    adress_line = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    postal_code = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    telephone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_adress", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "order_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    payment_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    account_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_payment", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    descriprion = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    role = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false, defaultValueSql: "('empty')"),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password_hash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    password_salt = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    verified_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    verification_token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    password_reset_token = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    reset_token_expires = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    category_id = table.Column<int>(type: "int", nullable: false),
                    discount_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_discount",
                        column: x => x.discount_id,
                        principalTable: "discount",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_product_product_category",
                        column: x => x.category_id,
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_details",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false, defaultValueSql: "((1))"),
                    payment_id = table.Column<int>(type: "int", nullable: true),
                    adress_id = table.Column<int>(type: "int", nullable: true),
                    total = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_details_order_adress",
                        column: x => x.adress_id,
                        principalTable: "order_adress",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_order_details_order_payment",
                        column: x => x.payment_id,
                        principalTable: "order_payment",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_order_details_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_adress",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    adress_line = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    city = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    postal_code = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    telephone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_adress", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_adress_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    payment_type = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    account_no = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_payment_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cart_item",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cart_item", x => x.id);
                    table.ForeignKey(
                        name: "FK_cart_item_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cart_item_user",
                        column: x => x.user_id,
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "image",
                columns: table => new
                {
                    id = table.Column<int>(name: "id ", type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    image_name = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_image_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<int>(type: "int", nullable: false),
                    product_id = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_order_details",
                        column: x => x.order_id,
                        principalTable: "order_details",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_items_product",
                        column: x => x.product_id,
                        principalTable: "product",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_cart_item_product_id",
                table: "cart_item",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_cart_item_user_id",
                table: "cart_item",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_image_product_id",
                table: "image",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_adress_id",
                table: "order_details",
                column: "adress_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_payment_id",
                table: "order_details",
                column: "payment_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_details_user_id",
                table: "order_details",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_product_id",
                table: "order_items",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_category_id",
                table: "product",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_product_discount_id",
                table: "product",
                column: "discount_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_adress_user_id",
                table: "user_adress",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_payment_user_id",
                table: "user_payment",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cart_item");

            migrationBuilder.DropTable(
                name: "image");

            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "user_adress");

            migrationBuilder.DropTable(
                name: "user_payment");

            migrationBuilder.DropTable(
                name: "order_details");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "order_adress");

            migrationBuilder.DropTable(
                name: "order_payment");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "discount");

            migrationBuilder.DropTable(
                name: "product_category");
        }
    }
}
